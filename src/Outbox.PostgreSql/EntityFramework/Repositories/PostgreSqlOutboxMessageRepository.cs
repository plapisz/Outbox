using Microsoft.EntityFrameworkCore;
using Outbox.Entities;
using Outbox.PostgreSql.Options;
using Outbox.Repositories;
using Outbox.Time;

namespace Outbox.PostgreSql.EntityFramework.Repositories;

internal sealed class PostgreSqlOutboxMessageRepository : IOutboxMessageRepository
{
    private readonly PostgreSqlOptions _options;
    private readonly IClock _clock;

    public PostgreSqlOutboxMessageRepository(PostgreSqlOptions options, IClock clock)
    {
        // DOTO: Solve dependencies hell and inject DbContext directly
        _options = options;
        _clock = clock;
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
        return await dbContext.OutboxMessages
            .Where(x => x.NextAttemptAt != null || x.NextAttemptAt > _clock.CurrentDate())
            .ToListAsync();
    }

    public async Task UpdateAsync(OutboxMessage outboxMessage)
    {
        using var dbContext = new OutboxDbContext(_options.ConnectionString);
        dbContext.OutboxMessages.Update(outboxMessage);
        await dbContext.SaveChangesAsync();
    }
}
