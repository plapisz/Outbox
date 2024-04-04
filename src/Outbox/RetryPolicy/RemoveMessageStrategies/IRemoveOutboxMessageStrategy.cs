using Outbox.Entities;

namespace Outbox.RetryPolicy.RemoveMessageStrategies;

internal interface IRemoveOutboxMessageStrategy
{
    bool UsePoisonQueue { get; }
    Task RemoveMessageAsync(OutboxMessage message);
}
