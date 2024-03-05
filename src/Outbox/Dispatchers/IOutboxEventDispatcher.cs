using Outbox.Types;

namespace Outbox.Dispatchers;

internal interface IOutboxEventDispatcher
{
    Task DispatchOutboxEventAsync(OutboxEventSource outboxEventSource);
}
