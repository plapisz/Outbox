using FluentAssertions;
using NSubstitute;
using Outbox.Entities;
using Outbox.Repositories;
using Outbox.RetryPolicy.RemoveMessageStrategies;
using Xunit;

namespace Outbox.Tests.Unit.RetryPolicy.RemoveMessageStrategies;

public class RemoveOutboxMessageDirectlyStrategyTests
{
    [Fact]
    public void use_poison_queue_should_return_false()
    {
        var outboxMessageRepository = Substitute.For<IOutboxMessageRepository>();
        var removeOutboxMessageDirectlyStrategy = new RemoveOutboxMessageDirectlyStrategy(outboxMessageRepository);

        var result = removeOutboxMessageDirectlyStrategy.UsePoisonQueue;

        result.Should().BeFalse();
    }

    [Fact]
    public async Task remove_message_async_should_call_outbox_message_repository_delete_async()
    {
        var outboxMessageRepository = Substitute.For<IOutboxMessageRepository>();
        var removeOutboxMessageDirectlyStrategy = new RemoveOutboxMessageDirectlyStrategy(outboxMessageRepository);
        var message = new OutboxMessage("Outbox.Tests.Unit.Shared.Events.OrderCreated",
            "{\"Id\":\"814b3ef3-8fcc-4af6-bf5d-9f6562b886de\",\"Number\":\"test-1\",\"CreatedAt\":\"2024-03-19T17:41:11.2140516+01:00\",\"IsConfirmed\":false,\"ConfirmedAt\":null,\"CustomerEmail\":\"johndoe@exmail.com\"",
            DateTime.UtcNow);

        await removeOutboxMessageDirectlyStrategy.RemoveMessageAsync(message);

        await outboxMessageRepository.Received(1).DeleteAsync(message);
    }
}
