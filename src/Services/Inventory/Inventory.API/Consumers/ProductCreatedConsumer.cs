using Common.Events;
using Inventory.API.Entities;
using Inventory.API.Persistence;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Inventory.API.Consumers;

public class ProductCreatedConsumer(InventoryDbContext db) : IConsumer<ProductCreated>
{
    public async Task Consume(ConsumeContext<ProductCreated> context)
    {
        var evt = context.Message;

        // Check we haven't already processed this event (idempotency)
        var exists = await db.InventoryItems
            .AnyAsync(i => i.ProductId == evt.ProductId);

        if (exists)
        {
            Console.WriteLine($"[Inventory] Already processed ProductId: {evt.ProductId} — skipping.");
            return;
        }

        // Create inventory record with 0 stock
        var item = new InventoryItem(evt.ProductId);
        db.InventoryItems.Add(item);
        await db.SaveChangesAsync();

        Console.WriteLine($"[Inventory] Created inventory for product: {evt.Name} (Id: {evt.ProductId})");
    }
}
