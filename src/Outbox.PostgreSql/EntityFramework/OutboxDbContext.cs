using Microsoft.EntityFrameworkCore;
using Outbox.Entities;

namespace Outbox.PostgreSql.EntityFramework;

internal sealed class OutboxDbContext : DbContext
{
    public DbSet<OutboxMessage> OutboxMessages { get; set; }
    public DbSet<PoisonQueueItem> PoisonQueueItems { get; set; }

    public OutboxDbContext(DbContextOptions<OutboxDbContext> options) : base(options)
    {
    }

    internal OutboxDbContext(string connectionString) : base(GetOptions(connectionString))
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Constants.SchemaNames.Outbox);
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }

    private static DbContextOptions GetOptions(string connectionString) 
        => NpgsqlDbContextOptionsBuilderExtensions.UseNpgsql(new DbContextOptionsBuilder(), connectionString).Options;
}
