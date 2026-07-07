using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Entities;

namespace Ordering.Infrastructure.Persistence;

public class OrderingDbContext : DbContext
{
    public OrderingDbContext(DbContextOptions<OrderingDbContext> options)
        : base(options)
    {
    }

    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(order =>
        {
            order.HasKey(o => o.Id);
            order.Property(o => o.Status).HasConversion<string>();

            // OrderItem is an owned entity — stored in the same table or a separate table
            // Here we use a separate table (OwnsMany) for clarity
            order.OwnsMany(o => o.Items, item =>
            {
                item.WithOwner().HasForeignKey("OrderId");
                item.HasKey(i => i.Id);
                item.ToTable("OrderItems");
            });
        });
    }
}
