using System.Text.Json;
using Basket.Domain.Entities;
using Basket.Domain.Repositories;
using StackExchange.Redis;

namespace Basket.Infrastructure.Repositories;

public class CartRepository(IConnectionMultiplexer redis) : ICartRepository
{
    private readonly IDatabase _db = redis.GetDatabase();
    private static readonly TimeSpan CartTtl = TimeSpan.FromHours(1);

    private static string Key(Guid userId) => $"basket:{userId}";

    public async Task<Cart?> GetCartAsync(Guid userId)
    {
        var data = await _db.StringGetAsync(Key(userId));
        if (data.IsNullOrEmpty) return null;

        return JsonSerializer.Deserialize<Cart>(data!);
    }

    public async Task SaveCartAsync(Cart cart)
    {
        var json = JsonSerializer.Serialize(cart);
        // StringSetAsync with expiry — sets a 1-hour sliding TTL
        await _db.StringSetAsync(Key(cart.UserId), json, CartTtl);
    }

    public async Task DeleteCartAsync(Guid userId)
    {
        await _db.KeyDeleteAsync(Key(userId));
    }
}
