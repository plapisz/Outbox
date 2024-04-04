namespace Outbox.RetryPolicy.RemoveMessageStrategies.Resolvers;

internal interface IRemoveOutboxMessageStrategyResolver
{
    IRemoveOutboxMessageStrategy Resolve(bool usePoisonQueue);
}
