using Common.Services.Startup;

var builder = WebApplication.CreateBuilder(args);
builder.AddDefaults();

var app = builder.Build();
app.UseDefaults();
app.MapControllers();

app.Run();