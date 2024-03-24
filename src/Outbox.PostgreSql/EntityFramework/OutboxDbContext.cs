using Microsoft.EntityFrameworkCore;
using Outbox.Entities;

namespace Outbox.PostgreSql.EntityFramework;

internal sealed class OutboxDbContext : DbContext
{
    public DbSet<OutboxMessage> OutboxMessages { get; set; }

    public OutboxDbContext(DbContextOptions<OutboxDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Constants.SchemaNames.Outbox);
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
