using Outbox.Entities;
using Outbox.Repositories;

namespace Outbox.RetryPolicy.RemoveMessageStrategies;

internal sealed class RemoveOutboxMessageDirectlyStrategy : IRemoveOutboxMessageStrategy
{
    private readonly IOutboxMessageRepository _outboxMessageRepository;

    public RemoveOutboxMessageDirectlyStrategy(IOutboxMessageRepository outboxMessageRepository)
    {
        _outboxMessageRepository = outboxMessageRepository;
    }

    public bool UsePoisonQueue => false;

    public async Task RemoveMessageAsync(OutboxMessage message)
    {
        await _outboxMessageRepository.DeleteAsync(message);
    }
}
