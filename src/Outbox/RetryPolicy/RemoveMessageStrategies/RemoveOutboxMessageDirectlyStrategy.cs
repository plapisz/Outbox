using Outbox.Entities;

namespace Outbox.RetryPolicy.RemoveMessageStrategies;

internal sealed class RemoveOutboxMessageDirectlyStrategy : IRemoveOutboxMessageStrategy
{
    public bool UsePoisonQueue => false;

    public Task RemoveMessageAsync(OutboxMessage message)
    {
        throw new NotImplementedException();
    }
}
