using Microsoft.Extensions.DependencyInjection;
using Outbox.Dispatchers;
using Outbox.Serializers;
using Outbox.Time;
using Outbox.Types;

namespace Outbox;

public static class Extensions
{
    public static IServiceCollection AddOutbox(this IServiceCollection services)
    {
        services.AddSingleton<IOutboxEventDispatcher, OutboxEventDispatcher>();
        services.AddSingleton<IClock, Clock>();
        services.AddSingleton<IOutboxEventSerializer, JsonOutboxEventSerializer>();

        using var serviceProvider = services.BuildServiceProvider();
        var outboxEventDispatcher = serviceProvider.GetRequiredService<IOutboxEventDispatcher>();
        OutboxEventSource.OutboxEventsDispatched += outboxEventDispatcher.DispatchOutboxEventAsync;

        return services;
    }
}
