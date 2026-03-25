using Microsoft.EntityFrameworkCore;
using TechShop.Application.Features.Products.Queries;
using TechShop.Application.Interfaces;
using TechShop.Infrastructure.Persistence;
using TechShop.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(GetProductsQuery).Assembly));

builder.Services.AddScoped<IProductRepository, ProductRepository>();
    
var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
