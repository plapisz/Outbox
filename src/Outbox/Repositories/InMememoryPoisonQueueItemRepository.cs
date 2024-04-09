using Outbox.Entities;

namespace Outbox.Repositories;

internal sealed class InMememoryPoisonQueueItemRepository : IPoisonQueueItemRepository
{
    private static readonly List<PoisonQueueItem> _poisonQueueItems = [];

    public Task AddAsync(PoisonQueueItem poisonQueueItem)
    {
        _poisonQueueItems.Add(poisonQueueItem);
        return Task.CompletedTask;
    }
}
