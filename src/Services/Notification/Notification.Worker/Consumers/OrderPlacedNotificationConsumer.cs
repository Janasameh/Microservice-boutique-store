using Common.Events;
using MassTransit;

namespace Notification.Worker.Consumers;

// Listens for OrderPlaced — simulates sending "order received" email
public class OrderPlacedNotificationConsumer(ILogger<OrderPlacedNotificationConsumer> logger)
    : IConsumer<OrderPlaced>
{
    public Task Consume(ConsumeContext<OrderPlaced> context)
    {
        var evt = context.Message;

        logger.LogInformation(
            "📧 [Notification] ORDER RECEIVED — Sending email to {UserEmail}",
            evt.UserEmail);

        logger.LogInformation(
            "   Subject: Your order #{OrderId} has been received!",
            evt.OrderId);

        logger.LogInformation(
            "   Body: Hi! We received your order of {ItemCount} item(s) totalling {Total:C}. We'll confirm it shortly.",
            evt.Items.Count, evt.TotalAmount);

        return Task.CompletedTask;
    }
}
