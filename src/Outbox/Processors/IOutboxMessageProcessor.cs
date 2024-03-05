namespace Outbox.Processors;

internal interface IOutboxMessageProcessor
{
    Task ProcessAsync();
}
