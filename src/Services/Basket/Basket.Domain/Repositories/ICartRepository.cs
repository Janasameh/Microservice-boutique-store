using Basket.Domain.Entities;

namespace Basket.Domain.Repositories;

public interface ICartRepository
{
    Task<Cart?> GetCartAsync(Guid userId);
    Task SaveCartAsync(Cart cart);
    Task DeleteCartAsync(Guid userId);
}
