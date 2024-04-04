using Outbox.Entities;

namespace Outbox.RetryPolicy.RemoveMessageStrategies;

internal sealed class MoveOutboxMessageToPoisonQueueStrategy : IRemoveOutboxMessageStrategy
{
    public bool UsePoisonQueue => true;

    public Task RemoveMessageAsync(OutboxMessage message)
    {
        throw new NotImplementedException();
    }
}
