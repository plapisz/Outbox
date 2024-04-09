using Microsoft.Extensions.DependencyInjection;
using Outbox.Configuration;
using Outbox.Dispatchers;
using Outbox.Events;
using Outbox.Events.Handlers;
using Outbox.Processors;
using Outbox.Repositories;
using Outbox.RetryPolicy.RemoveMessageStrategies.Resolvers;
using Outbox.RetryPolicy.RemoveMessageStrategies;
using Outbox.Serializers;
using Outbox.Services;
using Outbox.Time;
using Outbox.Types;
using Outbox.RetryPolicy.NextRetryAttemptsStrategies;
using Outbox.RetryPolicy.NextRetryAttemptsStrategies.Resolvers;
using Outbox.RetryPolicy.Options;

namespace Outbox;

public static class Extensions
{
    public static IServiceCollection AddOutbox(this IServiceCollection services, Action<IOutboxConfigurator> configurator = default)
    {
        // TODO: Check services lifetime scope
        services.AddSingleton<IOutboxEventDispatcher, OutboxEventDispatcher>();
        services.AddSingleton<IOutboxMessageProcessor, OutboxMessageProcessor>();
        services.AddSingleton<IClock, Clock>();
        services.AddSingleton<IOutboxEventSerializer, JsonOutboxEventSerializer>();
        services.AddHostedService<OutboxMessageService>();

        services.AddSingleton<IRemoveOutboxMessageStrategy, RemoveOutboxMessageDirectlyStrategy>();
        services.AddSingleton<IRemoveOutboxMessageStrategy, MoveOutboxMessageToPoisonQueueStrategy>();
        services.AddSingleton<IRemoveOutboxMessageStrategyResolver, RemoveOutboxMessageStrategyResolver>();
        services.AddSingleton<INextRetryAttemptsStrategy, NotSetNextRetryAttemptsStrategy>();
        services.AddSingleton<INextRetryAttemptsStrategy, RegularNextRetryAttemptsStrategy>();
        services.AddSingleton<INextRetryAttemptsStrategy, ExponentialNextRetryAttemptsStrategy>();
        services.AddSingleton<INextRetryAttemptsStrategyResolver, NextRetryAttemptsStrategyResolver>();

        var outboxConfigurator = new OutboxConfigurator(services);
        if (configurator == default)
        {
            outboxConfigurator.RegisterOutboxMessageRepository<InMemoryOutboxMessageRepository>();
            outboxConfigurator.RegisterPoisonQueueItemRepository<InMememoryPoisonQueueItemRepository>();
            services.AddSingleton(new RetryPolicyOptions
            {
                MaxRetryCount = 0,
                NextRetryAttemptsMode = NextRetryAttemptsModeOptions.NotSet,
                UsePoisonQueue = false,
            });
        }
        configurator?.Invoke(outboxConfigurator);

        using var serviceProvider = services.BuildServiceProvider();
        var outboxEventDispatcher = serviceProvider.GetRequiredService<IOutboxEventDispatcher>();
        OutboxEventSource.OutboxEventsDispatched += outboxEventDispatcher.DispatchOutboxEventAsync;

        return services;
    }

    public static IServiceCollection AddOutboxEventHandler<TOutboxEvent, TOutboxEventHandler>(this IServiceCollection services)
        where TOutboxEvent : class, IOutboxEvent
        where TOutboxEventHandler : class, IOutboxEventHandler<TOutboxEvent>
    {
        services.AddScoped<IOutboxEventHandler<TOutboxEvent>, TOutboxEventHandler>();

        return services;
    }
}
