using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechShop.Application.Features.Products.Queries;
using TechShop.Application.Common;
using TechShop.Domain.Entities;

namespace TechShop.API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ProductsController(IMediator mediator) : ControllerBase
{

private readonly IMediator _mediator = mediator;

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _mediator.Send(new GetProductsQuery());
        return Ok(ApiResponse<IEnumerable<Product>>.Ok(products, "Productos obtenidos exitosamente"));
    }

}