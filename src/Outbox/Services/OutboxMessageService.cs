using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Outbox.Processors;

namespace Outbox.Services;

internal sealed class OutboxMessageService : BackgroundService
{
    private IServiceProvider _serviceProvider;

    public OutboxMessageService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // DOTO: Add period to configuration
        using var timer = new PeriodicTimer(TimeSpan.FromMinutes(1));
        while (await timer.WaitForNextTickAsync(stoppingToken))
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var outboxMessageProcessor = _serviceProvider.GetRequiredService<IOutboxMessageProcessor>();
                await outboxMessageProcessor.ProcessAsync();
            }            
        }
    }
}
