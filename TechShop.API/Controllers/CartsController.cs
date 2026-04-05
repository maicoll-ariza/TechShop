using MediatR;
using System.Security.Claims;
using TechShop.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using TechShop.Application.Common;
using Microsoft.AspNetCore.Authorization;
using TechShop.Application.Features.Carts.DTOs;
using TechShop.Application.Features.Carts.Queries;
using TechShop.Application.Features.Carts.Commands;

namespace TechShop.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetCart()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var cart = await mediator.Send(new GetCartQuery(userId));
        return Ok(ApiResponse<Cart>.Ok(cart, "Carrito obtenido satisfactoriamente"));
    }

    [HttpDelete("items/{cartItemId}")]
    [Authorize]
    public async Task<IActionResult> DeleteItemCart([FromRoute] int cartItemId)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        
        var cart = await mediator.Send(new RemoveFromCartCommand(cartItemId, userId));

        return Ok(ApiResponse<Cart>.Ok(cart, "Item eliminado satisfactoriamente"));
    }

    [HttpPost("items")]
    [Authorize]
    public async Task<IActionResult> AddItem([FromBody] AddToCartRequest request)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var cart = await mediator.Send(new AddToCartCommand(userId, request.ProductId, request.Quantity));
        return Ok(ApiResponse<Cart>.Ok(cart, "Producto agregado al carrito"));
    }

    [HttpPut("items/{cartItemId}")]
    [Authorize]
    public async Task<IActionResult> UpdateItem([FromRoute] int cartItemId, [FromBody] UpdateCartItemRequest request)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var cart = await mediator.Send(new UpdateCartItemCommand(cartItemId, userId, request.Quantity));
        return Ok(ApiResponse<Cart>.Ok(cart, "Item actualizado correctamente"));
    }
}