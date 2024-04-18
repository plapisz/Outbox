using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Outbox.Entities;

namespace Outbox.PostgreSql.EntityFramework.Configurations;

internal sealed class PoisonQueueItemConfiguration : IEntityTypeConfiguration<PoisonQueueItem>
{
    public void Configure(EntityTypeBuilder<PoisonQueueItem> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Type).HasMaxLength(256).IsRequired();
        builder.Property(x => x.Data).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();

        builder.ToTable(nameof(PoisonQueueItem), Constants.SchemaNames.Outbox);
    }
}
