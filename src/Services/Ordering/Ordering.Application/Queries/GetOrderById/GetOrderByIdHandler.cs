using MediatR;
using Ordering.Domain.Repositories;

namespace Ordering.Application.Queries.GetOrderById;

public class GetOrderByIdHandler(IOrderRepository orderRepository)
    : IRequestHandler<GetOrderByIdQuery, OrderResponse?>
{
    public async Task<OrderResponse?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetByIdAsync(request.OrderId);
        if (order is null) return null;

        return new OrderResponse(
            order.Id,
            order.UserId,
            order.UserEmail,
            order.Status,
            order.TotalAmount,
            order.CreatedAt,
            order.Items.Select(i => new OrderItemResponse(
                i.ProductId, i.ProductName, i.Quantity, i.UnitPrice
            )).ToList()
        );
    }
}
