using Common.Events;
using MassTransit;
using Ordering.Domain.Entities;
using Ordering.Domain.Repositories;
using Ordering.Infrastructure.Persistence;

namespace Ordering.API.Consumers;

// This consumer listens for OrderPlaced events published by the Basket service.
// It creates the Order record in the database and then auto-confirms it
// (simulating instant payment approval — in a real system you'd wait for payment gateway).
public class OrderPlacedConsumer(
    IOrderRepository orderRepository,
    IPublishEndpoint publishEndpoint,
    ILogger<OrderPlacedConsumer> logger)
    : IConsumer<OrderPlaced>
{
    public async Task Consume(ConsumeContext<OrderPlaced> context)
    {
        var evt = context.Message;

        // Create the order domain entity
        var orderItems = evt.Items.Select(i =>
            new OrderItem(i.ProductId, i.ProductName, i.Quantity, i.UnitPrice)
        ).ToList();

        var order = new Order(evt.OrderId, evt.UserId, evt.UserEmail, evt.TotalAmount, orderItems);
        await orderRepository.AddAsync(order);

        // Auto-confirm: transition to Confirmed state
        order.Confirm();
        await orderRepository.SaveChangesAsync();

        logger.LogInformation(
            "[Ordering] Order {OrderId} created and confirmed for User {UserId}. Total: {TotalAmount:C}",
            order.Id, order.UserId, order.TotalAmount);

        // Publish OrderConfirmed — Basket will clear the cart, Notification will send email
        await publishEndpoint.Publish(new OrderConfirmed(
            order.Id,
            order.UserId,
            order.UserEmail,
            order.TotalAmount
        ), context.CancellationToken);
    }
}
