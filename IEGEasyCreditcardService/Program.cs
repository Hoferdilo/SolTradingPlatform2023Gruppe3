
using Common.Services.Startup;
using IEGEasyCreditcardService.Services;

namespace IEGEasyCreditcardService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.AddDefaults();
            AddServices(builder);

            var app = builder.Build();
            app.UseDefaults();
            app.MapControllers();
            app.Run();
        }

        public static void AddServices(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ICreditcardValidator, CreditcardValidator>();
        }
    }
}