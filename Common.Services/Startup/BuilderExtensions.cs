using System.Net.Sockets;
using Consul;
using Consul.Filtering;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Common.Services.Startup;

public static class BuilderExtensions
{
    public static ILogger GetSerilog()
    {
        return new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();
    }
    
    public static WebApplicationBuilder AddDefaults(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddHealthChecks();

        builder.Services.AddSingleton<IConsulClient>(consul => new ConsulClient(consulConfig =>
        {
            consulConfig.Address = new Uri("http://consul:8500");
        }));
        

        return builder;
    }

    public static async Task<IApplicationBuilder> UseDefaults(this IApplicationBuilder app, string serviceName)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseAuthorization();

        app.UseHealthChecks("/health");

        var consul = app.ApplicationServices.GetService<IConsulClient>();
        var ip = await System.Net.Dns.GetHostAddressesAsync(System.Net.Dns.GetHostName());
        var ipString = ip.FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork).ToString();

        var services = await consul.Agent.Services();
        if (services != null)
        {
            if( services.Response.Values.Any(x => x.Address == ipString && x.Service == serviceName) )
            {
                return app;
            }
        }

        await consul.Agent.ServiceRegister(new AgentServiceRegistration
        {
            ID = Guid.NewGuid().ToString(),
            Address = ipString,
            Port = 80,
            Name = serviceName,
            Check = new AgentServiceCheck
            {
                Name = "/health",
                HTTP = $"http://{ipString}/health",
                Interval = TimeSpan.FromSeconds(15)
            }
        });

        return app;
    }
}