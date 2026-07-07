using Basket.Domain.Entities;
using MediatR;

namespace Basket.Application.Commands.UpsertBasket;

// Command: replaces the entire cart for a user (add/update items)
public record UpsertBasketCommand(
    Guid UserId,
    string UserEmail,
    List<CartItemDto> Items
) : IRequest<Cart>;

public record CartItemDto(
    Guid ProductId,
    string ProductName,
    decimal UnitPrice,
    int Quantity
);
