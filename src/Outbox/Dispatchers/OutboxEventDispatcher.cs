using Microsoft.Extensions.DependencyInjection;
using Outbox.Entities;
using Outbox.Repositories;
using Outbox.Serializers;
using Outbox.Time;
using Outbox.Types;

namespace Outbox.Dispatchers;

internal sealed class OutboxEventDispatcher : IOutboxEventDispatcher
{
    private readonly IClock _clock;
    private readonly IOutboxEventSerializer _serializer;
    private readonly IOutboxMessageRepository _outboxMessageRepository;

    public OutboxEventDispatcher(IClock clock, IOutboxEventSerializer serializer, IOutboxMessageRepository outboxMessageRepository)
    {
        _clock = clock;
        _serializer = serializer;
        _outboxMessageRepository = outboxMessageRepository;
    }

    public async Task DispatchOutboxEventAsync(OutboxEventSource outboxEventSource)
    {
        var outboxEvents = outboxEventSource.OutboxEvents;
        if (outboxEvents == null || !outboxEvents.Any())
        {
            return;
        }

        foreach (var outboxEvent in outboxEvents)
        {
            var type = outboxEvent.GetType().Name;
            var data = _serializer.Serialize(outboxEvent);
            var now = _clock.CurrentDate();

            var message = new OutboxMessage(type, data, now);
            await _outboxMessageRepository.AddAsync(message);
        }
    }
}
