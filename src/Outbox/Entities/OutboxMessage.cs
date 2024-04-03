namespace Outbox.Entities;

public sealed class OutboxMessage
{
    public Guid Id { get; private set; }
    public string Type { get; private set; }
    public string Data { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public uint AttemptsCount { get; private set; }
    public DateTime? LastAttemptAt { get; private set; }
    public DateTime? NextAttemptAt { get; private set; }

    private OutboxMessage()
    {

    }

    internal OutboxMessage(string type, string data, DateTime createdAt)
    {
        Id = Guid.NewGuid();
        Type = type;
        Data = data;
        CreatedAt = createdAt;
        AttemptsCount = 0;
        LastAttemptAt = null;
        NextAttemptAt = null;
    }

    public void IncrementAttempsCount()
    {
        AttemptsCount++;
    }
}
