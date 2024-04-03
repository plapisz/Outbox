using Outbox.Entities;
using Outbox.Time;

namespace Outbox.Repositories;

internal sealed class InMemoryOutboxMessageRepository : IOutboxMessageRepository
{
    private static readonly List<OutboxMessage> _outboxMessages = [];
    private readonly IClock _clock;

    public InMemoryOutboxMessageRepository(IClock clock)
    {
        _clock = clock;
    }

    public Task AddAsync(OutboxMessage outboxMessage)
    {
        _outboxMessages.Add(outboxMessage);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(OutboxMessage outboxMessage)
    {
        return Task.CompletedTask;
    }

    public Task DeleteAsync(OutboxMessage outboxMessage)
    {
        _outboxMessages.Remove(outboxMessage);
        return Task.CompletedTask;
    }

    public Task<IEnumerable<OutboxMessage>> GetAllToProcessAsync()
    {
        var result = _outboxMessages.Where(x => x.NextAttemptAt is null || x.NextAttemptAt > _clock.CurrentDate());
        return Task.FromResult(result);
    }
}