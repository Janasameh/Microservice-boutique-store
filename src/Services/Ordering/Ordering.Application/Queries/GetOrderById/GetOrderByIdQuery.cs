using Ordering.Domain.Enums;
using MediatR;

namespace Ordering.Application.Queries.GetOrderById;

public record GetOrderByIdQuery(Guid OrderId) : IRequest<OrderResponse?>;

public record OrderResponse(
    Guid Id,
    Guid UserId,
    string UserEmail,
    OrderStatus Status,
    decimal TotalAmount,
    DateTime CreatedAt,
    List<OrderItemResponse> Items
);

public record OrderItemResponse(
    Guid ProductId,
    string ProductName,
    int Quantity,
    decimal UnitPrice
);
