using Basket.Domain.Entities;
using MediatR;

namespace Basket.Application.Queries.GetBasket;

public record GetBasketQuery(Guid UserId) : IRequest<Cart?>;
