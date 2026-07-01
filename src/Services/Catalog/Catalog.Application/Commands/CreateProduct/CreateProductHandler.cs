using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using Common.Events;
using MassTransit;
using MediatR;

namespace Catalog.Application.Commands.CreateProduct;

public class CreateProductHandler(
    IProductRepository repository,
    IPublishEndpoint publishEndpoint) : IRequestHandler<CreateProductCommand, Guid>
{
    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product(
            request.Name, request.Description, request.Price,
            request.ImageFile, request.Category, request.Color,
            request.Collection, request.AvailableSizes);

        await repository.AddAsync(product);
        await repository.SaveChangesAsync();

        // Publish event to RabbitMQ — fire and forget
        await publishEndpoint.Publish(new ProductCreated(
            product.Id,
            product.Name,
            product.Category,
            product.Price,
            product.AvailableSizes), cancellationToken);

        return product.Id;
    }
}
