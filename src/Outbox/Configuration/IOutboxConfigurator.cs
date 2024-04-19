using Microsoft.Extensions.DependencyInjection;
using Outbox.Repositories;
using Outbox.RetryPolicy.Options;

namespace Outbox.Configuration;

public interface IOutboxConfigurator
{
    IServiceCollection Services { get; }
    RetryPolicyOptions RetryPolicyOptions { get; set; }

    IOutboxConfigurator RegisterOutboxMessageRepository<T>() where T : class, IOutboxMessageRepository;
    IOutboxConfigurator RegisterPoisonQueueItemRepository<T>() where T : class, IPoisonQueueItemRepository;
}
