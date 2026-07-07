using MediatR;
using Ordering.Application.Queries.GetOrderById;
using Ordering.Domain.Repositories;

namespace Ordering.Application.Queries.GetOrdersByUser;

public class GetOrdersByUserHandler(IOrderRepository orderRepository)
    : IRequestHandler<GetOrdersByUserQuery, List<OrderResponse>>
{
    public async Task<List<OrderResponse>> Handle(GetOrdersByUserQuery request, CancellationToken cancellationToken)
    {
        var orders = await orderRepository.GetByUserIdAsync(request.UserId);

        return orders.Select(order => new OrderResponse(
            order.Id,
            order.UserId,
            order.UserEmail,
            order.Status,
            order.TotalAmount,
            order.CreatedAt,
            order.Items.Select(i => new OrderItemResponse(
                i.ProductId, i.ProductName, i.Quantity, i.UnitPrice
            )).ToList()
        )).ToList();
    }
}
