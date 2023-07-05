
using Common.Services.Startup;
using Serilog;

namespace MeiShop;

public class Program
{
    public static void Main(string[] args)
    {
        Log.Logger = BuilderExtensions.GetSerilog();
            
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.AddDefaults();
        builder.Services.AddHttpClient();

        var app = builder.Build();
        app.UseDefaults();

        app.MapControllers();

        app.Run();
    }
}