using Outbox.Events;

namespace Outbox.Tests.Unit.Shared.Events;

public sealed record OrderConfirmed(Guid OrderId, string OrderNumber, DateTime OrderConfirmationDate, string CustomerEmail) : IOutboxEvent;