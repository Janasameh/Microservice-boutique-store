namespace Common.Events;

public record ProductCreated(
    Guid ProductId,
    string Name,
    string Category,
    decimal Price,
    string AvailableSizes
);