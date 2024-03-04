using Outbox.Entities;

namespace Outbox.Repositories;

public interface IOutboxMessageRepository
{
    Task Add(OutboxMessage outboxMessage);
}
