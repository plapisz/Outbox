using Outbox.Events;

namespace Outbox.Samples.Api.Events;

public sealed record OrderConfirmed(Guid OrderId, string OrderNumber, DateTime OrderConfirmationDate, string CustomerEmail) : IOutboxEvent;