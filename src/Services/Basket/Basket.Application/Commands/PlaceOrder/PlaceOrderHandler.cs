using Basket.Domain.Repositories;
using Common.Events;
using MassTransit;
using MediatR;

namespace Basket.Application.Commands.PlaceOrder;

public class PlaceOrderHandler(
    ICartRepository cartRepository,
    IPublishEndpoint publishEndpoint)
    : IRequestHandler<PlaceOrderCommand, Guid>
{
    public async Task<Guid> Handle(PlaceOrderCommand request, CancellationToken cancellationToken)
    {
        var cart = await cartRepository.GetCartAsync(request.UserId);

        if (cart is null || cart.Items.Count == 0)
            throw new InvalidOperationException("Cannot place order: basket is empty.");

        var orderId = Guid.NewGuid();

        // Publish the OrderPlaced event to RabbitMQ
        // Ordering service will consume it and create the Order record
        await publishEndpoint.Publish(new OrderPlaced(
            orderId,
            cart.UserId,
            cart.UserEmail,
            cart.Items.Select(i => new OrderItemDto(
                i.ProductId,
                i.ProductName,
                i.Quantity,
                i.UnitPrice
            )).ToList(),
            cart.TotalPrice
        ), cancellationToken);

        // Note: we do NOT clear the cart here.
        // The cart will be cleared when OrderConfirmed is received (eventual consistency).

        return orderId;
    }
}
