using Outbox.Entities;

namespace Outbox.Repositories;

public interface IPoisonQueueItemRepository
{
    Task AddAsync(PoisonQueueItem poisonQueueItem);
}
