namespace Outbox.RetryPolicy.Options;

public enum NextRetryAttemptsModeOptions
{
    NotSet = 0,
    Regular = 1,
    Exponential = 2,
}