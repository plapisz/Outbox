using Microsoft.EntityFrameworkCore;
using Outbox.Entities;
using Outbox.Repositories;

namespace Outbox.PostgreSql.EntityFramework.Repositories;

internal sealed class PostgreSqlOutboxMessageRepository : IOutboxMessageRepository
{
    private readonly OutboxDbContext _dbContext;

    public PostgreSqlOutboxMessageRepository(OutboxDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(OutboxMessage outboxMessage)
    {
        await _dbContext.OutboxMessages.AddAsync(outboxMessage);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(OutboxMessage outboxMessage)
    {
        _dbContext.OutboxMessages.Remove(outboxMessage);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<OutboxMessage>> GetAllAsync()
    {
        return await _dbContext.OutboxMessages.ToListAsync();
    }
}
