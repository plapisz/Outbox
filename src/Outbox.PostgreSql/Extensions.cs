using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Outbox.Configuration;
using Outbox.PostgreSql.EntityFramework;
using Outbox.PostgreSql.EntityFramework.Repositories;
using Outbox.Repositories;

namespace Outbox.PostgreSql;

public static class Extensions
{
    public static void PostgreSql(this IOutboxConfigurator configurator, string connectionString)
    {
        configurator.Services.AddDbContext<OutboxDbContext>(options => options.UseNpgsql(connectionString));
        configurator.Services.AddScoped<IOutboxMessageRepository, PostgreSqlOutboxMessageRepository>();
    }
}
