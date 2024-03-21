using Outbox.Events.Handlers;

namespace Outbox.Tests.Unit.Shared.Events.Handlers;

internal sealed class OrderCreatedHandler : IOutboxEventHandler<OrderCreated>
{
    public Task HandleAsync(OrderCreated @event)
    {
        return Task.CompletedTask;
    }
}
