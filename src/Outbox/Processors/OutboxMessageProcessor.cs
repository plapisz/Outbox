using Outbox.Events;
using Outbox.Repositories;
using Outbox.Serializers;
using System.Reflection;

namespace Outbox.Processors;

internal sealed class OutboxMessageProcessor : IOutboxMessageProcessor
{
    private readonly IOutboxMessageRepository _outboxMessageRepository;
    private readonly IOutboxEventSerializer _outboxEventSerializer;
    private readonly IServiceProvider _serviceProvider;

    public OutboxMessageProcessor(IOutboxMessageRepository outboxMessageRepository, IOutboxEventSerializer outboxEventSerializer, IServiceProvider serviceProvider)
    {
        _outboxMessageRepository = outboxMessageRepository;
        _outboxEventSerializer = outboxEventSerializer;
        _serviceProvider = serviceProvider;
    }

    public async Task ProcessAsync()
    {
        var messages = await _outboxMessageRepository.GetAllAsync();
        if (!messages.Any())
        {
            return;
        }

        foreach (var message in messages)
        {
            var type = Assembly.GetExecutingAssembly().GetTypes().SingleOrDefault(x => x.Name == message.Type);
            var @event = (IOutboxEvent)_outboxEventSerializer.Deserialize(message.Data, type);

            // Find and call handler
            // When processing successfully then remove from database
            // What when fail?
        }
    }
}
