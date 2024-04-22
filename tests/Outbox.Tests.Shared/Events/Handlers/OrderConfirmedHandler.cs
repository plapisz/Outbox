using Outbox.Events.Handlers;

namespace Outbox.Tests.Shared.Events.Handlers;

public sealed class OrderConfirmedHandler : IOutboxEventHandler<OrderConfirmed>
{
    public Task HandleAsync(OrderConfirmed @event)
    {
        return Task.CompletedTask;
    }
}
