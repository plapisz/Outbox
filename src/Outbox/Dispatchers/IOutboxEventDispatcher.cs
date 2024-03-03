using Outbox.Types;

namespace Outbox.Dispatchers;

internal interface IOutboxEventDispatcher
{
    Task DispatchOutboxEvent(OutboxEventSource sender);
}
