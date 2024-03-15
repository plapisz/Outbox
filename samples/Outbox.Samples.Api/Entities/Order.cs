using Outbox.Samples.Api.Events;
using Outbox.Types;

namespace Outbox.Samples.Api.Entities;

public sealed class Order : OutboxEventSource
{
    public Guid Id { get; }
    public string Number { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public bool IsConfirmed { get; private set; }
    public DateTime? ConfirmedAt { get; private set; }
    public string CustomerEmail { get; private set; }

    public Order(Guid id, string number, DateTime createdAt, string customerEmail)
    {
        Id = id;
        Number = number;
        CreatedAt = createdAt;
        IsConfirmed = false;
        ConfirmedAt = null;
        CustomerEmail = customerEmail;

        AddOutboxEvent(new OrderCreated(Id, Number, CreatedAt, CustomerEmail));
    }

    public void Confirm(DateTime confirmedAt)
    {
        IsConfirmed = true;
        ConfirmedAt = confirmedAt;

        AddOutboxEvent(new OrderConfirmed(Id, Number, ConfirmedAt.Value, CustomerEmail));
    }
}

