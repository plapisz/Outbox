using Outbox.RetryPolicy.Options;

namespace Outbox.RetryPolicy.NextRetryAttemptsStrategies.Resolvers;

internal sealed class NextRetryAttemptsStrategyResolver : INextRetryAttemptsStrategyResolver
{
    private readonly IEnumerable<INextRetryAttemptsStrategy> _nextRetryAttemptsStrategies;

    public NextRetryAttemptsStrategyResolver(IEnumerable<INextRetryAttemptsStrategy> nextRetryAttemptsStrategies)
    {
        _nextRetryAttemptsStrategies = nextRetryAttemptsStrategies;
    }

    public INextRetryAttemptsStrategy Resolve(NextRetryAttemptsModeOptions nextRetryAttemptsMode)
        => _nextRetryAttemptsStrategies.Single(x => x.NextRetryAttemptsMode == nextRetryAttemptsMode);
}
