using Common.Services.Startup;
using Consul;
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

        var consulClient = app.Services.GetService<IConsulClient>();

        await consulClient.KV.Put(new KVPair("productAccessKey")
        {
            Value = Guid.NewGuid().ToByteArray(),
        });

        await app.RunAsync();
    }
}
