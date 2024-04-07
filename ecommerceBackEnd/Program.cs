using ecommerceBackEnd.Repository;
using ecommerceBackEnd.Repository.Interfaces;
using ecommerceBackEnd.Service;

var builder = WebApplication.CreateBuilder(args);


CreateCustomServices(builder.Services);

builder.Configuration.AddJsonFile("appsettings.json");

var app = builder.Build();
app.MapControllers();
app.UseCors(builder =>
{
    builder.AllowAnyOrigin();
    builder.AllowAnyMethod();
    builder.AllowAnyHeader();
});
app.Run();

void CreateCustomServices(IServiceCollection services)
{
    services.AddSingleton<IDBContext, DBContext>();
    services.AddScoped<IProductService, ProductService>();
    services.AddScoped<IProductRepo, ProductRepo>();
    
    services.AddControllers();
}


