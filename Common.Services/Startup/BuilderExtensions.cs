using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Services.Startup;

public static class BuilderExtensions
{
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

        app.UseHttpsRedirection();

        app.UseAuthorization();

        return app;
    }
}