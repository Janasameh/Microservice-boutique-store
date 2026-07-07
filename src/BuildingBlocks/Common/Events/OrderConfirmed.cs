namespace Common.Events;

public record OrderConfirmed(
    Guid OrderId,
    Guid UserId,
    string UserEmail,
    decimal TotalAmount
);
