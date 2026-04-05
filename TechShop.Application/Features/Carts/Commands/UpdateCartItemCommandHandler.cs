


using MediatR;
using TechShop.Domain.Entities;
using TechShop.Domain.Exceptions;
using TechShop.Application.Interfaces;

namespace TechShop.Application.Features.Carts.Commands;
public class UpdateCartItemCommandHandler : IRequestHandler<UpdateCartItemCommand, Cart>
{

    private readonly ICartRepository _cartRepository;

    public UpdateCartItemCommandHandler(ICartRepository cartRepository)
    {
        _cartRepository = cartRepository;
    }

    public async Task<Cart> Handle(UpdateCartItemCommand request, CancellationToken cancellationToken)
    {

        var existingItem = await _cartRepository.GetCartItemAsync(request.CartItemId);

        if (existingItem == null)
            throw new BusinessException($"No existe el item con ID: {request.CartItemId}");


       if (request.Quantity <= 0)
        {
            await _cartRepository.RemoveItemAsync(existingItem);
        }
        else
        {
            existingItem.Quantity = request.Quantity;
            await _cartRepository.UpdateItemAsync(existingItem);
        }

        return await _cartRepository.GetCartByUserIdAsync(request.UserId)
            ?? throw new BusinessException("Error al obtener el carrito actualizado");
    }
}