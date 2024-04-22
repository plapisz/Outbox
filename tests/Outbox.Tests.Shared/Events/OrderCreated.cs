using Outbox.Events;

namespace Outbox.Tests.Shared.Events;

public sealed record OrderCreated(Guid OrderId, string OrderNumber, DateTime OrderCreationDate, string CustomerEmail) : IOutboxEvent;
