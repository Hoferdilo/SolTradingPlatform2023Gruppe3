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

        return builder;
    }

    public static IApplicationBuilder UseDefaults(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseAuthorization();

        return app;
    }
}