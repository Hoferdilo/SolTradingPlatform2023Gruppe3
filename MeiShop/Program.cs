
using Common.Services.Startup;

namespace MeiShop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.AddDefaults();

            var app = builder.Build();
            app.UseDefaults();

            app.MapControllers();

            app.Run();
        }
    }
}