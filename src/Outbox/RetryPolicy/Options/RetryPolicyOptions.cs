namespace Outbox.RetryPolicy.Options;

public sealed class RetryPolicyOptions
{
    public uint MaxRetryCount { get; set; }
    public NextRetryAttemptsModeOptions NextRetryAttemptsMode { get; set; }
    public bool UsePoisonQueue { get; set; }
}
