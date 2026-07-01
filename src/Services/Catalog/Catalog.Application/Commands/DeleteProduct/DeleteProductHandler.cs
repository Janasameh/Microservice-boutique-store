using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Commands.DeleteProduct;

public class DeleteProductHandler(IProductRepository repository) : IRequestHandler<DeleteProductCommand, bool>
{
    public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await repository.GetByIdAsync(request.Id);
        if (product is null) return false;

        await repository.DeleteAsync(product);
        await repository.SaveChangesAsync();
        return true;
    }
}
