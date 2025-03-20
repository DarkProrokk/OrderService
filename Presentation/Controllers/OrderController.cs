using Application.Commands;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController(IMediator mediator): ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
    {
        var orderGuid = await mediator.Send(command);
        return Ok(new { OrderGuid = orderGuid });
    }


    [HttpPut("cancel/")]
    public async Task<IActionResult> CancelOrder([FromBody] CancelOrderCommand command)
    {
        var result = await mediator.Send(command);
        if (result.IsSuccess) return Ok(result);
        return BadRequest(result);
    }
}