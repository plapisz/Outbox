using Outbox.Entities;
using Outbox.PostgreSql.Options;
using Outbox.Repositories;

namespace Outbox.PostgreSql.EntityFramework.Repositories;

internal class PostgreSqlPoisonQueueItemRepository : IPoisonQueueItemRepository
{
    private readonly PostgreSqlOptions _options;

    public PostgreSqlPoisonQueueItemRepository(PostgreSqlOptions options)
    {
        // DOTO: Solve dependencies hell and inject DbContext directly
        _options = options;
    }

    public async Task AddAsync(PoisonQueueItem poisonQueueItem)
    {
        using var dbContext = new OutboxDbContext(_options.ConnectionString);
        await dbContext.PoisonQueueItems.AddAsync(poisonQueueItem);
        await dbContext.SaveChangesAsync();
    }
}
