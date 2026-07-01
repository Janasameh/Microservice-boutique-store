using Catalog.Application.Responses;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Queries.GetProductById;

public class GetProductByIdHandler(IProductRepository repository)
    : IRequestHandler<GetProductByIdQuery, ProductResponse?>
{
    public async Task<ProductResponse?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await repository.GetByIdAsync(request.Id);
        if (product is null) return null;

        return new ProductResponse(
            product.Id, product.Name, product.Description, product.Price,
            product.ImageFile, product.Category, product.Color,
            product.Collection, product.AvailableSizes);
    }
}
