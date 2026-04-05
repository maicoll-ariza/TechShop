using MediatR;
using TechShop.Domain.Entities;

namespace TechShop.Application.Features.Carts.Commands;

public record AddToCartCommand(int UserId, int ProductId, int Quantity) : IRequest<Cart>;