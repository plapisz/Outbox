using Microsoft.Extensions.DependencyInjection;
using Outbox.Configuration;
using Outbox.PostgreSql.Repositories;
using Outbox.Repositories;

namespace Outbox.PostgreSql;

public static class Extensions
{
    public static void PostgreSql(this IOutboxConfigurator configurator)
    {
        configurator.Services.AddScoped<IOutboxMessageRepository, PostgreSqlOutboxMessageRepository>();
    }
}
