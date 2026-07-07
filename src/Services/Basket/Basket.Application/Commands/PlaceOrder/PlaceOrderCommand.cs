using MediatR;

namespace Basket.Application.Commands.PlaceOrder;

// Command: triggers order placement — publishes OrderPlaced event
public record PlaceOrderCommand(Guid UserId) : IRequest<Guid>;
