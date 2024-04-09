namespace Outbox.Entities;

public sealed class PoisonQueueItem
{
    public Guid Id { get; private set; }
    public string Type { get; private set; }
    public string Data { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private PoisonQueueItem()
    {

    }

    internal PoisonQueueItem(string type, string data, DateTime createdAt)
    {
        Id = Guid.NewGuid();
        Type = type;
        Data = data;
        CreatedAt = createdAt;
    }
}
