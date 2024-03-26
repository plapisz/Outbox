using Microsoft.Extensions.Hosting;
using Outbox.Processors;

namespace Outbox.Services;

internal sealed class OutboxMessageService : BackgroundService
{
    private IOutboxMessageProcessor _outboxMessageProcessor;

    public OutboxMessageService(IOutboxMessageProcessor outboxMessageProcessor)
    {
        _outboxMessageProcessor = outboxMessageProcessor;
    }

    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // DOTO: Add period to configuration
        using var timer = new PeriodicTimer(TimeSpan.FromMinutes(1));
        while (await timer.WaitForNextTickAsync(stoppingToken))
        {
            await _outboxMessageProcessor.ProcessAsync();
        }
    }
}
