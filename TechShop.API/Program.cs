using Serilog;
using Microsoft.EntityFrameworkCore;
using TechShop.Application.Interfaces;
using TechShop.Infrastructure.Persistence;
using TechShop.Infrastructure.Repositories;
using TechShop.Application.Features.Products.Queries;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(GetProductsQuery).Assembly));

builder.Services.AddScoped<IProductRepository, ProductRepository>();
    
var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();

try
{
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "La aplicación terminó inesperadamente");
}
finally
{
    Log.CloseAndFlush();
}
