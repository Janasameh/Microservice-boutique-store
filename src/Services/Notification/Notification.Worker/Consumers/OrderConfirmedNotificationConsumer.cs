using Common.Events;
using MassTransit;

namespace Notification.Worker.Consumers;

// Listens for OrderConfirmed — simulates sending the order receipt/confirmation email
public class OrderConfirmedNotificationConsumer(ILogger<OrderConfirmedNotificationConsumer> logger)
    : IConsumer<OrderConfirmed>
{
    public Task Consume(ConsumeContext<OrderConfirmed> context)
    {
        var evt = context.Message;

        logger.LogInformation(
            "✅ [Notification] ORDER CONFIRMED — Sending confirmation email to {UserEmail}",
            evt.UserEmail);

        logger.LogInformation(
            "   Subject: Your order #{OrderId} is confirmed!",
            evt.OrderId);

        logger.LogInformation(
            "   Body: Great news! Your order of {Total:C} has been confirmed. Thank you for shopping with Boutique!",
            evt.TotalAmount);

        return Task.CompletedTask;
    }
}
