
using Common.Services.Startup;
using Serilog;

namespace MeiShop;

public class Program
{
    public static async Task Main(string[] args)
    {
        Log.Logger = BuilderExtensions.GetSerilog();
            
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.AddDefaults();
        builder.Services.AddHttpClient();

        var app = builder.Build();
        await app.UseDefaults("MeiShop");

        app.MapControllers();

        await app.RunAsync();
    }
}