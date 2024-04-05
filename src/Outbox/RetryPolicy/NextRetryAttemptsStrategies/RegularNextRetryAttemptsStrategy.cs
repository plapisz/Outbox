using Outbox.RetryPolicy.Options;

namespace Outbox.RetryPolicy.NextRetryAttemptsStrategies;

internal sealed class RegularNextRetryAttemptsStrategy : INextRetryAttemptsStrategy
{
    public NextRetryAttemptsModeOptions NextRetryAttemptsMode => NextRetryAttemptsModeOptions.Regular;

    public DateTime? GetNextAttemptAt(DateTime? lastAttemptAt, uint attemptsCount)
    {
        throw new NotImplementedException();
    }
}
