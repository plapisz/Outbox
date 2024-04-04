namespace Outbox.RetryPolicy.RemoveMessageStrategies.Resolvers;

internal sealed class RemoveOutboxMessageStrategyResolver : IRemoveOutboxMessageStrategyResolver
{
    private readonly IEnumerable<IRemoveOutboxMessageStrategy> _removeOutboxMessageStrategies;

    public RemoveOutboxMessageStrategyResolver(IEnumerable<IRemoveOutboxMessageStrategy> removeOutboxMessageStrategies)
    {
        _removeOutboxMessageStrategies = removeOutboxMessageStrategies;
    }

    public IRemoveOutboxMessageStrategy Resolve(bool usePoisonQueue)
        => _removeOutboxMessageStrategies.Single(x => x.UsePoisonQueue == usePoisonQueue);
}