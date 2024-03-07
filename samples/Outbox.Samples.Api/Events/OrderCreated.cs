using Outbox.Events;

namespace Outbox.Samples.Api.Events;

public sealed record OrderCreated(Guid OrderId, string OrderNumber, DateTime OrderCreationDate, string CustomerEmail) : IOutboxEvent;
