using MediatR;
using Ordering.Application.Queries.GetOrderById;

namespace Ordering.Application.Queries.GetOrdersByUser;

public record GetOrdersByUserQuery(Guid UserId) : IRequest<List<OrderResponse>>;
