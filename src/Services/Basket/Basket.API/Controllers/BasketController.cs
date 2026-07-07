using Basket.Application.Commands.PlaceOrder;
using Basket.Application.Commands.UpsertBasket;
using Basket.Application.Queries.GetBasket;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BasketController(ISender sender) : ControllerBase
{
    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetBasket(Guid userId)
    {
        var cart = await sender.Send(new GetBasketQuery(userId));
        if (cart is null) return NotFound($"No basket found for user {userId}.");
        return Ok(cart);
    }

    [HttpPut]
    public async Task<IActionResult> UpsertBasket([FromBody] UpsertBasketCommand command)
    {
        var cart = await sender.Send(command);
        return Ok(cart);
    }

    [HttpPost("{userId:guid}/checkout")]
    public async Task<IActionResult> Checkout(Guid userId)
    {
        var orderId = await sender.Send(new PlaceOrderCommand(userId));
        return Accepted(new { OrderId = orderId });
    }
}
