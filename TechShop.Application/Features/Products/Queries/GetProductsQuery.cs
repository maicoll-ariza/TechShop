using MediatR;
using TechShop.Application.Features.Products.DTOs;

namespace TechShop.Application.Features.Products.Queries;

public record GetProductsQuery() : IRequest<IEnumerable<ProductDto>>;