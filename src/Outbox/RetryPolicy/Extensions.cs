using Microsoft.Extensions.DependencyInjection;
using Outbox.Configuration;
using Outbox.RetryPolicy.Options;

namespace Outbox.RetryPolicy;

public static class Extensions
{
    public static IOutboxConfigurator WithRetryPolicy(this IOutboxConfigurator configurator, RetryPolicyOptions options)
    {
        configurator.RetryPolicyOptions = options;

        return configurator;
    }
}
