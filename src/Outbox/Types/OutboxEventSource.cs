using Outbox.Events;

namespace Outbox.Types;

public abstract class OutboxEventSource
{
    protected readonly Queue<IOutboxEvent> _outboxEvents = new();

    public IReadOnlyCollection<IOutboxEvent> OutboxEvents => _outboxEvents.ToList();

    protected void AddOutboxEvent(IOutboxEvent outboxEvent)
    {
        _outboxEvents.Enqueue(outboxEvent);
    }
}
