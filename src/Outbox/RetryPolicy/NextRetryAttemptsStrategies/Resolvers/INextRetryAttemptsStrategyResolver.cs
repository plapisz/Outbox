using Outbox.RetryPolicy.Options;

namespace Outbox.RetryPolicy.NextRetryAttemptsStrategies.Resolvers;

internal interface INextRetryAttemptsStrategyResolver
{
    INextRetryAttemptsStrategy Resolve(NextRetryAttemptsModeOptions nextRetryAttemptsMode);
}
