using Catalog.Application.Responses;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Queries.GetAllProducts;

public class GetProductsHandler(IProductRepository repository)
    : IRequestHandler<GetProductsQuery, IEnumerable<ProductResponse>>
{
    public async Task<IEnumerable<ProductResponse>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await repository.GetAllAsync();
        return products.Select(p => new ProductResponse(
            p.Id, p.Name, p.Description, p.Price,
            p.ImageFile, p.Category, p.Color, p.Collection, p.AvailableSizes));
    }
}
