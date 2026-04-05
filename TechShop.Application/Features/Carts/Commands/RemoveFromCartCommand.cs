

using MediatR;
using TechShop.Domain.Entities;

namespace TechShop.Application.Features.Carts.Commands;

public record RemoveFromCartCommand(int CartItemId, int UserId) : IRequest<Cart>;