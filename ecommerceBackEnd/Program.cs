using ecommerceBackEnd.Repository;
using ecommerceBackEnd.Repository.Interfaces;
using ecommerceBackEnd.Service;

var builder = WebApplication.CreateBuilder(args);


CreateCustomServices(builder.Services);

builder.Configuration.AddJsonFile("appsettings.json");

var app = builder.Build();
app.MapControllers();

app.Run();

void CreateCustomServices(IServiceCollection services)
{
    services.AddSingleton<IDBContext, DBContext>();
    services.AddScoped<IProductService, ProductService>();
    services.AddScoped<IProductRepo, ProductRepo>();
    
    services.AddControllers();
}


