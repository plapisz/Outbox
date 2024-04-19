namespace Outbox.RetryPolicy.Options;

public sealed class RetryPolicyOptions
{
    public uint MaxRetryCount { get; }
    public NextRetryAttemptsModeOptions NextRetryAttemptsMode { get; }
    public bool UsePoisonQueue { get; }

    public RetryPolicyOptions()
    {
        MaxRetryCount = 0;
        NextRetryAttemptsMode = NextRetryAttemptsModeOptions.NotSet;
        UsePoisonQueue = false;
    }

    public RetryPolicyOptions(uint maxRetryCount, NextRetryAttemptsModeOptions nextRetryAttemptsMode, bool usePoisonQueue)
    {
        MaxRetryCount = maxRetryCount;
        NextRetryAttemptsMode = nextRetryAttemptsMode;
        UsePoisonQueue = usePoisonQueue;
    }
}
