namespace TechShop.Application.Features.Carts.DTOs;

public class AddToCartRequest
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}
