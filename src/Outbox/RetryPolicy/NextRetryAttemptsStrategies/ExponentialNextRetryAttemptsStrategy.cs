using Outbox.RetryPolicy.Options;
using Outbox.Time;

namespace Outbox.RetryPolicy.NextRetryAttemptsStrategies;

internal sealed class ExponentialNextRetryAttemptsStrategy : INextRetryAttemptsStrategy
{
    private readonly IClock _clock;

    public ExponentialNextRetryAttemptsStrategy(IClock clock)
    {
        _clock = clock;
    }

    public NextRetryAttemptsModeOptions NextRetryAttemptsMode => NextRetryAttemptsModeOptions.Exponential;

    public DateTime? GetNextAttemptAt(DateTime? lastAttemptAt, uint attemptsCount)
    {
        lastAttemptAt ??= _clock.CurrentDate();

        return lastAttemptAt.Value.AddMinutes(attemptsCount * 2);
    }
}