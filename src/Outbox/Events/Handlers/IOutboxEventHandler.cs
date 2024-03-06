namespace Outbox.Events.Handlers;

public interface IOutboxEventHandler<in TEvent> where TEvent : class, IOutboxEvent
{
    Task HandleAsync(TEvent @event);
}
