using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using Catalog.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Repositories;

public class ProductRepository(CatalogDbContext context) : IProductRepository
{
    public async Task<IEnumerable<Product>> GetAllAsync()
        => await context.Products.ToListAsync();

    public async Task<Product?> GetByIdAsync(Guid id)
        => await context.Products.FindAsync(id);

    public async Task AddAsync(Product product)
        => await context.Products.AddAsync(product);

    public async Task UpdateAsync(Product product)
        => context.Products.Update(product);

    public async Task DeleteAsync(Product product)
        => context.Products.Remove(product);

    public async Task<bool> SaveChangesAsync()
        => await context.SaveChangesAsync() > 0;
}
