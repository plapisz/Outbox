using Outbox.Events.Handlers;

namespace Outbox.Tests.Shared.Events.Handlers;

public sealed class OrderCreatedHandler : IOutboxEventHandler<OrderCreated>
{
    public Task HandleAsync(OrderCreated @event)
    {
        return Task.CompletedTask;
    }
}
