namespace Catalog.Application.Responses;

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
