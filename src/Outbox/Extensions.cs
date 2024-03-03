using Microsoft.Extensions.DependencyInjection;
using Outbox.Dispatchers;
using Outbox.Types;

namespace Outbox;

public static class Extensions
{
    public static IServiceCollection AddOutbox(this IServiceCollection services)
    {
        var outboxEventDispatcher = new OutboxEventDispatcher();
        OutboxEventSource.OutboxEventsDispatched += outboxEventDispatcher.DispatchOutboxEvent;
        services.AddSingleton<IOutboxEventDispatcher>(outboxEventDispatcher);

        return services;
    }
}
