using Outbox.RetryPolicy.Options;

namespace Outbox.RetryPolicy.NextRetryAttemptsStrategies;

internal interface INextRetryAttemptsStrategy
{
    NextRetryAttemptsModeOptions NextRetryAttemptsMode { get; }
    DateTime? GetNextAttemptAt(DateTime? lastAttemptAt, uint attemptsCount);
}
