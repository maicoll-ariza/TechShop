using TechShop.Domain.Entities;

namespace TechShop.Application.Interfaces;

public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetByUserIdAsync(int userId);
    Task<Order?> GetByIdAsync(int id);
    Task<Order> CreateAsync(Order order);
    Task UpdateAsync(Order order);
}