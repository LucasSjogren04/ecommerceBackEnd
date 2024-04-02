var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();
app.MapControllers();

CreateCustomServices(builder.Services);
void CreateCustomServices(IServiceCollection services)
{
    services.AddControllers();
}

app.Run();
