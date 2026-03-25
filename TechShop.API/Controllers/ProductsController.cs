using MediatR;
using Microsoft.AspNetCore.Mvc;
using TechShop.Application.Features.Products.Queries;

namespace TechShop.API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ProductsController(IMediator mediator) : ControllerBase
{

private readonly IMediator _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _mediator.Send(new GetProductsQuery());
        return Ok(products);
    }

}