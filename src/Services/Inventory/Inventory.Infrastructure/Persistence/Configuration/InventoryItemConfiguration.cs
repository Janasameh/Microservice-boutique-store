using Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventory.Infrastructure.Persistence.Configuration;

public class InventoryItemConfiguration : IEntityTypeConfiguration<InventoryItem>
{
    public void Configure(EntityTypeBuilder<InventoryItem> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.ProductId)
            .IsRequired();

        builder.Property(p => p.Quantity)
            .IsRequired();

        builder.HasIndex(p => p.ProductId); // fast lookup by ProductId
    }
}