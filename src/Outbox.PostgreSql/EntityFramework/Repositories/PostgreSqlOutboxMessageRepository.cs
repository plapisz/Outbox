using Microsoft.EntityFrameworkCore;
using Outbox.Entities;
using Outbox.PostgreSql.Options;
using Outbox.Repositories;

namespace Outbox.PostgreSql.EntityFramework.Repositories;

internal sealed class PostgreSqlOutboxMessageRepository : IOutboxMessageRepository
{
    private readonly PostgreSqlOptions _options;

    public PostgreSqlOutboxMessageRepository(PostgreSqlOptions options)
    {
        // DOTO: Solve dependencies hell and inject DbContext directly
        _options = options;
    }

    public async Task AddAsync(OutboxMessage outboxMessage)
    {
        using var dbContext = new OutboxDbContext(_options.ConnectionString);
        await dbContext.OutboxMessages.AddAsync(outboxMessage);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(OutboxMessage outboxMessage)
    {
        using var dbContext = new OutboxDbContext(_options.ConnectionString);
        dbContext.OutboxMessages.Remove(outboxMessage);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<OutboxMessage>> GetAllToProcessAsync()
    {
        using var dbContext = new OutboxDbContext(_options.ConnectionString);
        return await dbContext.OutboxMessages.ToListAsync();
    }
}
