using Outbox.RetryPolicy.Options;
using Outbox.Time;

namespace Outbox.RetryPolicy.NextRetryAttemptsStrategies;

internal sealed class RegularNextRetryAttemptsStrategy : INextRetryAttemptsStrategy
{
    private readonly IClock _clock;

    public RegularNextRetryAttemptsStrategy(IClock clock)
    {
        _clock = clock;
    }

    public NextRetryAttemptsModeOptions NextRetryAttemptsMode => NextRetryAttemptsModeOptions.Regular;

    public DateTime? GetNextAttemptAt(DateTime? lastAttemptAt, uint attemptsCount)
    {
        lastAttemptAt ??= _clock.CurrentDate();

        return lastAttemptAt.Value.AddMinutes(1);
    }
}
