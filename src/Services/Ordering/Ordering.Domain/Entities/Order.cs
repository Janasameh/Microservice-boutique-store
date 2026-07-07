using Ordering.Domain.Enums;

namespace Ordering.Domain.Entities;

public class Order
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string UserEmail { get; private set; } = string.Empty;
    public OrderStatus Status { get; private set; }
    public decimal TotalAmount { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public List<OrderItem> Items { get; private set; } = new();

    private Order() { }  // for EF Core

    public Order(Guid id, Guid userId, string userEmail, decimal totalAmount, List<OrderItem> items)
    {
        Id = id;
        UserId = userId;
        UserEmail = userEmail;
        Status = OrderStatus.Pending;
        TotalAmount = totalAmount;
        CreatedAt = DateTime.UtcNow;
        Items = items;
    }

    public void Confirm()
    {
        if (Status != OrderStatus.Pending)
            throw new InvalidOperationException($"Cannot confirm order in status: {Status}");

        Status = OrderStatus.Confirmed;
    }

    public void Cancel()
    {
        if (Status == OrderStatus.Confirmed)
            throw new InvalidOperationException("Cannot cancel a confirmed order.");

        Status = OrderStatus.Cancelled;
    }
}
