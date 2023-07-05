using Common.Services.Startup;
using Serilog;


public class Program
{
    public static async Task Main(string[] args)
    {
        Log.Logger = BuilderExtensions.GetSerilog();

        var builder = WebApplication.CreateBuilder(args);
        builder.AddDefaults();

        var app = builder.Build();
        await app.UseDefaults("ProductService");
        app.MapControllers();

        await app.RunAsync();
    }
}
