using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Entities;
using Ordering.Domain.Repositories;
using Ordering.Infrastructure.Persistence;

namespace Ordering.Infrastructure.Repositories;

public class OrderRepository(OrderingDbContext context) : IOrderRepository
{
    public async Task<Order?> GetByIdAsync(Guid id)
        => await context.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == id);

    public async Task<IEnumerable<Order>> GetByUserIdAsync(Guid userId)
        => await context.Orders
            .Include(o => o.Items)
            .Where(o => o.UserId == userId)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();

    public async Task AddAsync(Order order)
        => await context.Orders.AddAsync(order);

    public async Task<bool> SaveChangesAsync()
        => await context.SaveChangesAsync() > 0;
}
