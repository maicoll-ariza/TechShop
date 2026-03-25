using MediatR;
using TechShop.Domain.Entities;

namespace TechShop.Application.Features.Products.Queries;

public record GetProductsQuery() : IRequest<IEnumerable<Product>>;