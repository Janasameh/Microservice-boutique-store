namespace Catalog.Domain.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Domain.Entities.Product>> GetAllAsync();
    Task<Domain.Entities.Product?> GetByIdAsync(Guid id);
    Task AddAsync(Domain.Entities.Product product);
    Task UpdateAsync(Domain.Entities.Product product);
    Task DeleteAsync(Domain.Entities.Product product);
    Task<bool> SaveChangesAsync();
}
