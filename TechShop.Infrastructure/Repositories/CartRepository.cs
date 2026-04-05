

using Microsoft.EntityFrameworkCore;
using TechShop.Application.Interfaces;
using TechShop.Domain.Entities;
using TechShop.Domain.Exceptions;
using TechShop.Infrastructure.Persistence;

namespace TechShop.Infrastructure.Repositories;
public class CartRepository(AppDbContext appDbContext) : ICartRepository
{
    public async Task<Cart?> GetCartByUserIdAsync(int id)
    {
        return await appDbContext.Carts
                .Include(c => c.Items)
                    .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(c => c.UserId == id);
    }

    public async Task<Cart> CreateCartAsync(Cart cart)
    {
        await appDbContext.Carts.AddAsync(cart);
        await appDbContext.SaveChangesAsync();
        return cart;
    }

    public async Task<CartItem> AddItemAsync(CartItem item)
    {
        await appDbContext.CartItems.AddAsync(item);
        await appDbContext.SaveChangesAsync();
        return item;
    }

    public async Task UpdateItemAsync(CartItem item)
    {
        appDbContext.CartItems.Update(item);
        await appDbContext.SaveChangesAsync();
    }

    public async Task RemoveItemAsync(CartItem cartItem)
    {
        appDbContext.CartItems.Remove(cartItem);
        await appDbContext.SaveChangesAsync();
    }

    public async Task ClearCartAsync(int cartId)
    {
        var items = await appDbContext.CartItems
            .Where(i => i.CartId == cartId)
            .ToListAsync();

        appDbContext.CartItems.RemoveRange(items);
        await appDbContext.SaveChangesAsync();
    }

    public async Task<CartItem?> GetCartItemAsync(int idCartItem)
    {
        return await appDbContext.CartItems
                .FirstOrDefaultAsync(ci => ci.Id == idCartItem);
    }
}