using TechShop.Domain.Enums;

namespace TechShop.Domain.Entities;

public class Order
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public decimal Total { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
}