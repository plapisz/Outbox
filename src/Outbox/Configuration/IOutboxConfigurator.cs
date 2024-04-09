using Microsoft.Extensions.DependencyInjection;
using Outbox.Repositories;

namespace Outbox.Configuration;

public interface IOutboxConfigurator
{
    IServiceCollection Services { get; }

    IOutboxConfigurator RegisterOutboxMessageRepository<T>() where T : class, IOutboxMessageRepository;
    IOutboxConfigurator RegisterPoisonQueueItemRepository<T>() where T : class, IPoisonQueueItemRepository;
}
