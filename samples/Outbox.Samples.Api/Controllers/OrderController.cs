using MediatR;
using Microsoft.AspNetCore.Mvc;
using Outbox.Samples.Api.Commands;

namespace Outbox.Samples.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly IMediator mediator;

    public OrderController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CreateOrder command, CancellationToken cancellationToken)
    {
        await mediator.Send(command with { Id = Guid.NewGuid() }, cancellationToken);

        return Ok();
    }
}
