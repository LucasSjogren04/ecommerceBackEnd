var builder = WebApplication.CreateBuilder(args);


CreateCustomServices(builder.Services);

builder.Configuration.AddJsonFile("appsettings.json");

var app = builder.Build();
app.MapControllers();


void CreateCustomServices(IServiceCollection services)
{
    services.AddControllers();
}

app.Run();
