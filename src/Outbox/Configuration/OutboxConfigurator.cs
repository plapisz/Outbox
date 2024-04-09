using Microsoft.Extensions.DependencyInjection;
using Outbox.Repositories;

namespace Outbox.Configuration;

internal sealed class OutboxConfigurator : IOutboxConfigurator
{
    public IServiceCollection Services { get; }

    public OutboxConfigurator(IServiceCollection services)
    {
        Services = services;
    }

    public IOutboxConfigurator RegisterOutboxMessageRepository<T>() where T : class, IOutboxMessageRepository
    {
        Services.AddSingleton<IOutboxMessageRepository, T>();

        return this;
    }

    public IOutboxConfigurator RegisterPoisonQueueItemRepository<T>() where T : class, IPoisonQueueItemRepository
    {
        Services.AddSingleton<IPoisonQueueItemRepository, T>();

        return this;
    }
}
