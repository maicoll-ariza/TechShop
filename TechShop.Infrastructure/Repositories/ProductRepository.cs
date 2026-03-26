using Microsoft.EntityFrameworkCore;
using TechShop.Application.Interfaces;
using TechShop.Domain.Entities;
using TechShop.Infrastructure.Persistence;

namespace TechShop.Infrastructure.Repositories; 

public class ProductRepository : IProductRepository
{
    
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Product> CreateAsync(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task DeleteAsync(int id)
    {
        Product? product = await _context.Products
        .FindAsync(id);

        if (product == null) return;

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        IList<Product> products = await _context.Products
            .AsNoTracking()
            .Include(p => p.Category)
            .ToListAsync();

        return products;
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _context.Products
        .Include(p => p.Category)
        .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task UpdateAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }
}