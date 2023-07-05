
using Common.Services.Startup;
using IEGEasyCreditcardService.Services;
using Serilog;

namespace IEGEasyCreditcardService;

public class Program
{
    public static async Task Main(string[] args)
    {
        Log.Logger = BuilderExtensions.GetSerilog();
            
        var builder = WebApplication.CreateBuilder(args);
        builder.AddDefaults();
        AddServices(builder);

        var app = builder.Build();
        await app.UseDefaults("IEGEasyCreditcardService");
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        await app.RunAsync();
    }

    public static void AddServices(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ICreditcardValidator, CreditcardValidator>();
    }
}