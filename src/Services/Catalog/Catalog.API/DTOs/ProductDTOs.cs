namespace Catalog.API.DTOs;

public record CreateProductRequest(
    string Name,
    string Description,
    decimal Price,
    string ImageFile,
    string Category,
    string Color,
    string Collection,
    string AvailableSizes
);

public record UpdateProductRequest(
    string Name,
    string Description,
    decimal Price,
    string ImageFile,
    string Category,
    string Color,
    string Collection,
    string AvailableSizes
);

public record ProductResponse(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    string ImageFile,
    string Category,
    string Color,
    string Collection,
    string AvailableSizes
);
