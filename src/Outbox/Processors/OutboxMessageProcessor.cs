using Outbox.Repositories;

namespace Outbox.Processors;

internal sealed class OutboxMessageProcessor : IOutboxMessageProcessor
{
    private readonly IOutboxMessageRepository _outboxMessageRepository;

    public OutboxMessageProcessor(IOutboxMessageRepository outboxMessageRepository)
    {
        _outboxMessageRepository = outboxMessageRepository;
    }

    public async Task ProcessAsync()
    {
        var messages = await _outboxMessageRepository.GetAllAsync();
        if (!messages.Any())
        {
            return;
        }


        foreach (var message in messages)
        {
            // Find and call handler
            // When processing successfully then remove from database
            // What when fail?
        }
    }
}
