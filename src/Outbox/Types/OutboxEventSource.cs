using Outbox.Events;

namespace Outbox.Types;

public abstract class OutboxEventSource
{
    protected readonly Queue<IOutboxEvent> _outboxEvents = new();

    internal delegate Task OutboxEventsDispatchedEventHandler(OutboxEventSource sender);

    internal static event OutboxEventsDispatchedEventHandler OutboxEventsDispatched;

    public IReadOnlyCollection<IOutboxEvent> OutboxEvents => _outboxEvents.ToList();

    protected void AddOutboxEvent(IOutboxEvent outboxEvent)
    {
        _outboxEvents.Enqueue(outboxEvent);
    }

    public async Task DispatchOutboxEventsAsync()
    {
        if (OutboxEventsDispatched is not null)
        {
            await OutboxEventsDispatched.Invoke(this);
        }
        
        _outboxEvents.Clear();
    }
}
