using Basket.Domain.Entities;
using Basket.Domain.Repositories;
using MediatR;

namespace Basket.Application.Commands.UpsertBasket;

public class UpsertBasketHandler(ICartRepository cartRepository)
    : IRequestHandler<UpsertBasketCommand, Cart>
{
    public async Task<Cart> Handle(UpsertBasketCommand request, CancellationToken cancellationToken)
    {
        var cart = new Cart
        {
            UserId = request.UserId,
            UserEmail = request.UserEmail,
            Items = request.Items.Select(i => new CartItem
            {
                ProductId = i.ProductId,
                ProductName = i.ProductName,
                UnitPrice = i.UnitPrice,
                Quantity = i.Quantity
            }).ToList()
        };

        await cartRepository.SaveCartAsync(cart);
        return cart;
    }
}
