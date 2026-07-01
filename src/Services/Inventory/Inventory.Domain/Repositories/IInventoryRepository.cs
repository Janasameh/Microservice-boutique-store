using Inventory.Domain.Entities;

namespace Inventory.Domain.Repositories {

    public interface IInventoryRepository
    {
     Task<InventoryItem?> GetItemByProductIdAsync(Guid productId);
     Task<IEnumerable<InventoryItem>> GetAllAsync();
     Task AddItemAsync(InventoryItem item);
     Task UpdateItemAsync(InventoryItem item);
     Task DeleteItemAsync(InventoryItem item);
     Task<bool> SaveChangesAsync();

    }
}
