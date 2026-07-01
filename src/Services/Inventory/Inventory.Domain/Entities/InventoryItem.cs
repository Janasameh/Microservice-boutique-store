
namespace Inventory.Domain.Entities;

 public class InventoryItem
 {

    public Guid Id {get; private set;}
    public Guid ProductId {get; private set;}
    public int Quantity {get; private set;}
    public DateTime CreatedAt {get; private set;}

    private InventoryItem() { }


    public  InventoryItem(Guid productId)
    {
        Id= Guid.NewGuid();
        ProductId = productId;
        Quantity = 0;
        CreatedAt = DateTime.UtcNow;
    } 

    public void AddStock(int quantity)
    {
        if (quantity <= 0 )
        {
            throw new ArgumentException("Quantity must be positive",nameof(quantity));
        }
        Quantity+= quantity;
    }
}