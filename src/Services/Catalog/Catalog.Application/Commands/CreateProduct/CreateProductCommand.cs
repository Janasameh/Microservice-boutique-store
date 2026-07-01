using MediatR;

namespace Catalog.Application.Commands.CreateProduct;

public record CreateProductCommand(
    string Name,
    string Description,
    decimal Price,
    string ImageFile,
    string Category,
    string Color,
    string Collection,
    string AvailableSizes
) : IRequest<Guid>;
