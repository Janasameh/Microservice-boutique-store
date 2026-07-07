namespace Common.Events;

public record OrderPlaced(
    Guid OrderId,
    Guid UserId,
    string UserEmail,
    List<OrderItemDto> Items,
    decimal TotalAmount
);

public record OrderItemDto(
    Guid ProductId,
    string ProductName,
    int Quantity,
    decimal UnitPrice
);
