using Basket.Domain.Repositories;
using Common.Events;
using MassTransit;

namespace Basket.API.Consumers;

// This consumer listens for OrderConfirmed events.
// When an order is confirmed, we can safely clear the user's cart.
public class OrderConfirmedConsumer(ICartRepository cartRepository, ILogger<OrderConfirmedConsumer> logger)
    : IConsumer<OrderConfirmed>
{
    public async Task Consume(ConsumeContext<OrderConfirmed> context)
    {
        var evt = context.Message;

        await cartRepository.DeleteCartAsync(evt.UserId);

        logger.LogInformation(
            "[Basket] Cart cleared for UserId: {UserId} after OrderId: {OrderId} was confirmed.",
            evt.UserId, evt.OrderId);
    }
}
