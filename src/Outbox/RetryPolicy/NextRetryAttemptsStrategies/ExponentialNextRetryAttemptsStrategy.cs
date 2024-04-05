using Outbox.RetryPolicy.Options;

namespace Outbox.RetryPolicy.NextRetryAttemptsStrategies;

internal sealed class ExponentialNextRetryAttemptsStrategy : INextRetryAttemptsStrategy
{
    public NextRetryAttemptsModeOptions NextRetryAttemptsMode => NextRetryAttemptsModeOptions.Exponential;

    public DateTime? GetNextAttemptAt(DateTime? lastAttemptAt, uint attemptsCount)
    {
        throw new NotImplementedException();
    }
}