namespace Outbox.Time;

internal sealed class Clock : IClock
{
    public DateTime CurrentDate() => DateTime.UtcNow;
}
