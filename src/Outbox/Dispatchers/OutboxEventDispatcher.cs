using Outbox.Types;

namespace Outbox.Dispatchers;

internal sealed class OutboxEventDispatcher : IOutboxEventDispatcher
{
    public Task DispatchOutboxEvent(OutboxEventSource sender)
    {
        return Task.CompletedTask;
    }
}
