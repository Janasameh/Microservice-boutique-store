namespace Basket.Domain.Entities;

public class Cart
{
    public Guid UserId { get; set; }
    public string UserEmail { get; set; } = string.Empty;
    public List<CartItem> Items { get; set; } = new();

    public decimal TotalPrice => Items.Sum(i => i.UnitPrice * i.Quantity);
}
