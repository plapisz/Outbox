using Outbox.Entities;

namespace Outbox.Repositories;

public interface IOutboxMessageRepository
{
    Task AddAsync(OutboxMessage outboxMessage);
    Task UpdateAsync(OutboxMessage outboxMessage);
    Task DeleteAsync(OutboxMessage outboxMessage);
    Task<IEnumerable<OutboxMessage>> GetAllToProcessAsync();
}
