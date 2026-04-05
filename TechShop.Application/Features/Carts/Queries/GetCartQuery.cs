using MediatR;
using TechShop.Domain.Entities;

namespace TechShop.Application.Features.Carts.Queries;

public record GetCartQuery(int UserId) : IRequest<Cart>;