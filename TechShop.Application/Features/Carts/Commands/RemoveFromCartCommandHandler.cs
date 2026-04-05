


using MediatR;
using TechShop.Domain.Entities;
using TechShop.Domain.Exceptions;
using TechShop.Application.Interfaces;

namespace TechShop.Application.Features.Carts.Commands;

public class RemoveFromCartCommandHandler : IRequestHandler<RemoveFromCartCommand, Cart>
{

    private readonly ICartRepository _cartRepository;

    public RemoveFromCartCommandHandler(ICartRepository cartRepository)
    {
        _cartRepository = cartRepository;
    }

    public async Task<Cart> Handle(RemoveFromCartCommand request, CancellationToken cancellationToken)
    {
        var existingItem = await _cartRepository.GetCartItemAsync(request.CartItemId);

        if (existingItem == null) 
            throw new BusinessException("No existe el Item que desea eliminar");

        await _cartRepository.RemoveItemAsync(existingItem);

        // Retornar carrito actualizado
        return await _cartRepository.GetCartByUserIdAsync(request.UserId)
            ?? throw new BusinessException("Error al obtener el carrito actualizado");
    
    }
}