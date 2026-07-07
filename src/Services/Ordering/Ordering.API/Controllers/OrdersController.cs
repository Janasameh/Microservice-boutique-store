using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Queries.GetOrderById;
using Ordering.Application.Queries.GetOrdersByUser;

namespace Ordering.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController(ISender sender) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var order = await sender.Send(new GetOrderByIdQuery(id));
        if (order is null) return NotFound();
        return Ok(order);
    }

    [HttpGet("user/{userId:guid}")]
    public async Task<IActionResult> GetByUser(Guid userId)
    {
        var orders = await sender.Send(new GetOrdersByUserQuery(userId));
        return Ok(orders);
    }
}
