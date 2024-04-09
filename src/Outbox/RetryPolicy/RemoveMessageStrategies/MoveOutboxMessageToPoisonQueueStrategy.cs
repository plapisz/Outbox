using Outbox.Entities;
using Outbox.Repositories;
using Outbox.Time;

namespace Outbox.RetryPolicy.RemoveMessageStrategies;

internal sealed class MoveOutboxMessageToPoisonQueueStrategy : IRemoveOutboxMessageStrategy
{
    private readonly IOutboxMessageRepository _outboxMessageRepository;
    private readonly IPoisonQueueItemRepository _poisonQueueItemRepository;
    private readonly IClock _clock;

    public MoveOutboxMessageToPoisonQueueStrategy(IOutboxMessageRepository outboxMessageRepository, IPoisonQueueItemRepository poisonQueueItemRepository, IClock clock)
    {
        _outboxMessageRepository = outboxMessageRepository;
        _poisonQueueItemRepository = poisonQueueItemRepository;
        _clock = clock;
    }

    public bool UsePoisonQueue => true;

    public async Task RemoveMessageAsync(OutboxMessage message)
    {
        var poisonQueueItem = new PoisonQueueItem(message.Type, message.Data, _clock.CurrentDate());
        await _poisonQueueItemRepository.AddAsync(poisonQueueItem);
        await _outboxMessageRepository.DeleteAsync(message);
    }
}
