using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Outbox.Configuration;
using Outbox.PostgreSql.EntityFramework;
using Outbox.PostgreSql.EntityFramework.Repositories;
using Outbox.PostgreSql.Options;
using Outbox.Repositories;

namespace Outbox.PostgreSql;

public static class Extensions
{
    public static void PostgreSql(this IOutboxConfigurator configurator, PostgreSqlOptions options)
    {
        configurator.Services.AddSingleton(options);
        configurator.Services.AddDbContext<OutboxDbContext>(builderOptions =>
        {
            builderOptions.UseNpgsql(options.ConnectionString, sqlOptionsBuilder =>
            {
                sqlOptionsBuilder.MigrationsHistoryTable(Constants.TableNames.MigrationsHistory, Constants.SchemaNames.Outbox);
            });
        });
        configurator.RegisterOutboxMessageRepository<PostgreSqlOutboxMessageRepository>();
        configurator.RegisterPoisonQueueItemRepository<PostgreSqlPoisonQueueItemRepository>();

        using var serviceProvider = configurator.Services.BuildServiceProvider();
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<OutboxDbContext>();
        dbContext.Database.Migrate();
    }
}
