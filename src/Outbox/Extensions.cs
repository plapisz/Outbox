using Microsoft.Extensions.DependencyInjection;
using Outbox.Configuration;
using Outbox.Dispatchers;
using Outbox.Events;
using Outbox.Events.Handlers;
using Outbox.Processors;
using Outbox.Repositories;
using Outbox.Serializers;
using Outbox.Services;
using Outbox.Time;
using Outbox.Types;

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

        var outboxConfigurator = new OutboxConfigurator(services);
        if (configurator == default)
        {
            outboxConfigurator.Register<InMemoryOutboxMessageRepository>();
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
