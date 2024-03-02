using Outbox.Events;

namespace Outbox.Tests.Unit.Shared.Events;

public sealed record OrderCreated(Guid OrderId, string OrderNumber, DateTime OrderCreationDate, string CustomerEmail) : IOutboxEvent;
