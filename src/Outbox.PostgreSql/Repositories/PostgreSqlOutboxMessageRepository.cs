using Outbox.Entities;
using Outbox.Repositories;

namespace Outbox.PostgreSql.Repositories;

internal sealed class PostgreSqlOutboxMessageRepository : IOutboxMessageRepository
{
    public Task AddAsync(OutboxMessage outboxMessage)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(OutboxMessage outboxMessage)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<OutboxMessage>> GetAllAsync()
    {
        throw new NotImplementedException();
    }
}
