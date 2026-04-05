using MediatR;
using TechShop.Application.Interfaces;
using TechShop.Domain.Entities;
using TechShop.Domain.Exceptions;

namespace TechShop.Application.Features.Carts.Commands;

public class AddToCartCommandHandler : IRequestHandler<AddToCartCommand, Cart>
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;

    public AddToCartCommandHandler(ICartRepository cartRepository, IProductRepository productRepository)
    {
        _cartRepository = cartRepository;
        _productRepository = productRepository;
    }

    public async Task<Cart> Handle(AddToCartCommand request, CancellationToken cancellationToken)
    {
        // Verificar que el producto existe
        var product = await _productRepository.GetByIdAsync(request.ProductId);
        if (product == null)
            throw new BusinessException($"El producto con ID {request.ProductId} no existe");

        // Verificar stock suficiente
        if (product.Stock < request.Quantity)
            throw new BusinessException($"Stock insuficiente. Disponible: {product.Stock}");

        // Obtener o crear el carrito
        var cart = await _cartRepository.GetCartByUserIdAsync(request.UserId);
        if (cart == null)
        {
            cart = await _cartRepository.CreateCartAsync(new Cart
            {
                UserId = request.UserId
            });
        }

        // Verificar si el producto ya está en el carrito
        var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == request.ProductId);
        if (existingItem != null)
        {
            existingItem.Quantity += request.Quantity;
            await _cartRepository.UpdateItemAsync(existingItem);
        }
        else
        {
            await _cartRepository.AddItemAsync(new CartItem
            {
                CartId = cart.Id,
                ProductId = request.ProductId,
                Quantity = request.Quantity
            });
        }

        // Retornar carrito actualizado
        return await _cartRepository.GetCartByUserIdAsync(request.UserId)
            ?? throw new BusinessException("Error al obtener el carrito actualizado");
    }
}