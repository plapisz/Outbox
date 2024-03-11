using Microsoft.Extensions.Hosting;

namespace Outbox.Services;

internal sealed class OutboxMessageService : BackgroundService
{
    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // DOTO: Add period to configuration
        using var timer = new PeriodicTimer(TimeSpan.FromMinutes(1));
        while (await timer.WaitForNextTickAsync(stoppingToken))
        {
            Console.WriteLine($"{DateTime.UtcNow} - processing....");
        }
    }
}
