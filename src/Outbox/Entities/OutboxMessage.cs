namespace Outbox.Entities;

public sealed class OutboxMessage
{
    public Guid Id { get; private set; }
    public string Type { get; private set; }
    public string Data { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private OutboxMessage()
    {

    }

    internal OutboxMessage(string type, string data, DateTime createdAt)
    {
        Id = Guid.NewGuid();
        Type = type;
        Data = data;
        CreatedAt = createdAt;
    }
}
