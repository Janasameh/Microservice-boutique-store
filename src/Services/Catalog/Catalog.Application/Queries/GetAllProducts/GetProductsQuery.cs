using Catalog.Application.Responses;
using MediatR;

namespace Catalog.Application.Queries.GetAllProducts;

public record GetProductsQuery() : IRequest<IEnumerable<ProductResponse>>;
