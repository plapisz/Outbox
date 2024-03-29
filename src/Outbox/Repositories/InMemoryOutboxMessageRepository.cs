﻿using Outbox.Entities;

namespace Outbox.Repositories;

internal sealed class InMemoryOutboxMessageRepository : IOutboxMessageRepository
{
    private static readonly List<OutboxMessage> _outboxMessages = [];

    public Task AddAsync(OutboxMessage outboxMessage)
    {
        _outboxMessages.Add(outboxMessage);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(OutboxMessage outboxMessage)
    {
        _outboxMessages.Remove(outboxMessage);
        return Task.CompletedTask;
    }

    public Task<IEnumerable<OutboxMessage>> GetAllAsync()
    {
        return Task.FromResult(_outboxMessages.AsEnumerable());
    }
}