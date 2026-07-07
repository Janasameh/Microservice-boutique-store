using Ordering.Domain.Entities;

namespace Ordering.Domain.Repositories;

public interface IOrderRepository
{
    Task<Order?> GetByIdAsync(Guid id);
    Task<IEnumerable<Order>> GetByUserIdAsync(Guid userId);
    Task AddAsync(Order order);
    Task<bool> SaveChangesAsync();
}
