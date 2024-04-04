using Microsoft.Extensions.DependencyInjection;
using Outbox.Events;
using Outbox.Events.Handlers;
using Outbox.Repositories;
using Outbox.RetryPolicy.Options;
using Outbox.RetryPolicy.RemoveMessageStrategies.Resolvers;
using Outbox.Serializers;

namespace Outbox.Processors;

internal sealed class OutboxMessageProcessor : IOutboxMessageProcessor
{
    private readonly IOutboxMessageRepository _outboxMessageRepository;
    private readonly IOutboxEventSerializer _outboxEventSerializer;
    private readonly IRemoveOutboxMessageStrategyResolver _removeOutboxMessageStrategyResolver;
    private readonly IServiceProvider _serviceProvider;
    private readonly RetryPolicyOptions _retryPolicyOptions;

    public OutboxMessageProcessor(IOutboxMessageRepository outboxMessageRepository, 
        IOutboxEventSerializer outboxEventSerializer,
        IRemoveOutboxMessageStrategyResolver removeOutboxMessageStrategyResolver,
        IServiceProvider serviceProvider, 
        RetryPolicyOptions retryPolicyOptions)
    {
        _outboxMessageRepository = outboxMessageRepository;
        _outboxEventSerializer = outboxEventSerializer;
        _removeOutboxMessageStrategyResolver = removeOutboxMessageStrategyResolver;
        _serviceProvider = serviceProvider;
        _retryPolicyOptions = retryPolicyOptions;
    }

    public async Task ProcessAsync()
    {
        var messages = await _outboxMessageRepository.GetAllToProcessAsync();
        if (!messages.Any())
        {
            return;
        }

        using var scope = _serviceProvider.CreateScope();
        foreach (var message in messages.ToList())
        {
            var type = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .SingleOrDefault(x => x.FullName == message.Type);
            var @event = (IOutboxEvent)_outboxEventSerializer.Deserialize(message.Data, type);

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
                if (message.AttemptsCount >= _retryPolicyOptions.MaxRetryCount)
                {
                    var removeOutboxMessageStrategy = _removeOutboxMessageStrategyResolver.Resolve(_retryPolicyOptions.UsePoisonQueue);
                    await removeOutboxMessageStrategy.RemoveMessageAsync(message);
                }

                message.IncrementAttempsCount();
                // Create policy to set  NextRetryAttempts depending on NextRetryAttemptsMode
                await _outboxMessageRepository.UpdateAsync(message);
            }
        }
    }
}
