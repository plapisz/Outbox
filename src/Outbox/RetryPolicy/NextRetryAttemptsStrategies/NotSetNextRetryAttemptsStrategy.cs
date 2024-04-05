using Outbox.RetryPolicy.Options;

namespace Outbox.RetryPolicy.NextRetryAttemptsStrategies;

internal sealed class NotSetNextRetryAttemptsStrategy : INextRetryAttemptsStrategy
{
    public NextRetryAttemptsModeOptions NextRetryAttemptsMode => NextRetryAttemptsModeOptions.NotSet;

    public DateTime? GetNextAttemptAt(DateTime? lastAttemptAt, uint attemptsCount)
    {
        throw new NotImplementedException();
    }
}
