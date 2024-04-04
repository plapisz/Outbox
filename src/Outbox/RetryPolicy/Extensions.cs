using Microsoft.Extensions.DependencyInjection;
using Outbox.Configuration;
using Outbox.RetryPolicy.Options;
using Outbox.RetryPolicy.RemoveMessageStrategies;
using Outbox.RetryPolicy.RemoveMessageStrategies.Resolvers;

namespace Outbox.RetryPolicy;

public static class Extensions
{
    public static IOutboxConfigurator WithRetryPolicy(this IOutboxConfigurator configurator, RetryPolicyOptions options)
    {
        configurator.Services.AddSingleton(options);
        configurator.Services.AddSingleton<IRemoveOutboxMessageStrategy, RemoveOutboxMessageDirectlyStrategy>();
        configurator.Services.AddSingleton<IRemoveOutboxMessageStrategy, MoveOutboxMessageToPoisonQueueStrategy>();
        configurator.Services.AddSingleton<IRemoveOutboxMessageStrategyResolver, RemoveOutboxMessageStrategyResolver>();

        return configurator;
    }
}
