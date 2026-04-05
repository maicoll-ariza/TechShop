

using MediatR;
using TechShop.Domain.Entities;
using TechShop.Application.Interfaces;
using TechShop.Domain.Exceptions;

namespace TechShop.Application.Features.Carts.Queries;
public class GetCartQueryHandler : IRequestHandler<GetCartQuery, Cart>
{

    private readonly ICartRepository _cartRepository;
    public GetCartQueryHandler(ICartRepository cartRepository)
    {
        _cartRepository = cartRepository;
    }

    public async Task<Cart> Handle(GetCartQuery request, CancellationToken cancellationToken)
    {
        var cart = await _cartRepository.GetCartByUserIdAsync(request.UserId);

        if (cart == null)
            throw new BusinessException($"No existe carrito todavía para el usuario especificado con ID: {request.UserId}");

        return cart;
    }
}