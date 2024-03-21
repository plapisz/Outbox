using Outbox.Events.Handlers;

namespace Outbox.Tests.Unit.Shared.Events.Handlers;

internal sealed class OrderConfirmedHandler : IOutboxEventHandler<OrderConfirmed>
{
    public Task HandleAsync(OrderConfirmed @event)
    {
        return Task.CompletedTask;
    }
}
