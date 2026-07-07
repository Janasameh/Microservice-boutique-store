using Basket.Domain.Entities;
using Basket.Domain.Repositories;
using MediatR;

namespace Basket.Application.Queries.GetBasket;

public class GetBasketHandler(ICartRepository cartRepository)
    : IRequestHandler<GetBasketQuery, Cart?>
{
    public async Task<Cart?> Handle(GetBasketQuery request, CancellationToken cancellationToken)
        => await cartRepository.GetCartAsync(request.UserId);
}
