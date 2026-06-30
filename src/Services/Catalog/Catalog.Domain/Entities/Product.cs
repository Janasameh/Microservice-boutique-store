
namespace Catalog.Domain.Entities;

public class Product
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public decimal Price { get; private set; }
    public string ImageFile { get; private set; } = string.Empty;
    public string Category { get; private set; } = string.Empty;
    public string Color { get; private set; } = string.Empty;
    public string Collection { get; private set; } = string.Empty;
    public string AvailableSizes { get; private set; } = string.Empty;


    private Product() { }  // for EF Core

    public Product(string name, string description, decimal price, string imageFile, string category, string color, string collection,string availableSizes)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        Price = price;
        ImageFile = imageFile;
        Category = category;
        Color = color;
        Collection = collection;
        AvailableSizes = availableSizes;
    }
}