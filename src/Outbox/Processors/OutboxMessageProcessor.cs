using Microsoft.Extensions.DependencyInjection;
using Outbox.Events;
using Outbox.Events.Handlers;
using Outbox.Repositories;
using Outbox.Serializers;

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

        foreach (var message in messages.ToList())
        {
            var type = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .SingleOrDefault(x => x.FullName == message.Type);
            var @event = (IOutboxEvent)_outboxEventSerializer.Deserialize(message.Data, type);

            using var scope = _serviceProvider.CreateScope();
            {
                var handlerType = typeof(IOutboxEventHandler<>).MakeGenericType(@event.GetType());
                var handler = scope.ServiceProvider.GetRequiredService(handlerType);

                try
                {
                    await (Task)handlerType
                        .GetMethod(nameof(IOutboxEventHandler<IOutboxEvent>.HandleAsync))
                        ?.Invoke(handler, new[] { @event });

                    await _outboxMessageRepository.DeleteAsync(message);
                }
                catch (Exception ex)
                {
                    // TODO: What when fail? Consider retry policy
                }
            }
        }
    }
}
