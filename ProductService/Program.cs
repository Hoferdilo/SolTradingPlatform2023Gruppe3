using Common.Services.Startup;
using Serilog;


public class Program
{
    public static void Main(string[] args)
    {
        Log.Logger = BuilderExtensions.GetSerilog();

        var builder = WebApplication.CreateBuilder(args);
        builder.AddDefaults();

        var app = builder.Build();
        app.UseDefaults();
        app.MapControllers();

        app.Run();
    }
}
