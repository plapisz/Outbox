using Microsoft.Extensions.DependencyInjection;
using Outbox.Repositories;

namespace Outbox.Configuration;

public interface IOutboxConfigurator
{
    IServiceCollection Services { get; }

    public IOutboxConfigurator Register<T>() where T : class, IOutboxMessageRepository;
}
