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


    [HttpDelete]
    public async Task<IActionResult> DeclineOrder([FromBody] DeclineOrderCommand command)
    {
        await mediator.Send(command);
        return Ok();
    }
}