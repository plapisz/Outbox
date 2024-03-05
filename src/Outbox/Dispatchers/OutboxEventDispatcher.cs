using Microsoft.Extensions.DependencyInjection;
using Outbox.Entities;
using Outbox.Repositories;
using Outbox.Serializers;
using Outbox.Time;
using Outbox.Types;

namespace Outbox.Dispatchers;

internal sealed class OutboxEventDispatcher : IOutboxEventDispatcher
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IClock _clock;
    private readonly IOutboxEventSerializer _serializer;

    public OutboxEventDispatcher(IServiceProvider serviceProvider, IClock clock, IOutboxEventSerializer serializer)
    {
        _serviceProvider = serviceProvider;
        _clock = clock;
        _serializer = serializer;
    }

    public async Task DispatchOutboxEventAsync(OutboxEventSource outboxEventSource)
    {
        var outboxEvents = outboxEventSource.OutboxEvents;
        if (outboxEvents == null || !outboxEvents.Any())
        {
            return;
        }

        using var scope = _serviceProvider.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<IOutboxMessageRepository>();
        foreach (var outboxEvent in outboxEvents)
        {
            var type = outboxEvent.GetType().Name;
            var data = _serializer.Serialize(outboxEvent);
            var now = _clock.CurrentDate();

            var message = new OutboxMessage(type, data, now);
            await repository.AddAsync(message);
        }
    }
}
