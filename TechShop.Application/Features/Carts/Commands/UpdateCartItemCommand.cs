

using MediatR;
using TechShop.Domain.Entities;

namespace TechShop.Application.Features.Carts.Commands;
public record UpdateCartItemCommand(int UserId, int CartItemId, int Quantity) : IRequest<Cart>;