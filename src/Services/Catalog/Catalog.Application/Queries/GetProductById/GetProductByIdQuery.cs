using Catalog.Application.Responses;
using MediatR;

namespace Catalog.Application.Queries.GetProductById;

public record GetProductByIdQuery(Guid Id) : IRequest<ProductResponse?>;
