using TechShop.Domain.Entities;

namespace TechShop.Application.Interfaces;

public interface ICartRepository
{
    Task<Cart?> GetCartByUserIdAsync(int userId);
    Task<Cart> CreateCartAsync(Cart cart);
    Task<CartItem> AddItemAsync(CartItem item);
    Task UpdateItemAsync(CartItem item);
    Task RemoveItemAsync(int cartItemId);
    Task ClearCartAsync(int cartId);
}