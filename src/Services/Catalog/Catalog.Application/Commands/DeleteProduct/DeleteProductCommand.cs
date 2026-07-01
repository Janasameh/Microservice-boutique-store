using MediatR;

namespace Catalog.Application.Commands.DeleteProduct;

public record DeleteProductCommand(Guid Id) : IRequest<bool>;
