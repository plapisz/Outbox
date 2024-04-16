using FluentAssertions;
using NSubstitute;
using Outbox.Entities;
using Outbox.Repositories;
using Outbox.RetryPolicy.RemoveMessageStrategies;
using Outbox.Time;
using Xunit;

namespace Outbox.Tests.Unit.RetryPolicy.RemoveMessageStrategies;

public class MoveOutboxMessageToPoisonQueueStrategyTests
{
    [Fact]
    public void use_poison_queue_should_return_true()
    {
        var outboxMessageRepository = Substitute.For<IOutboxMessageRepository>();
        var poisonQueueItemRepository = Substitute.For<IPoisonQueueItemRepository>();
        var clock = Substitute.For<IClock>();
        var moveOutboxMessageToPoisonQueueStrategy = new MoveOutboxMessageToPoisonQueueStrategy(outboxMessageRepository, poisonQueueItemRepository, clock);

        var result = moveOutboxMessageToPoisonQueueStrategy.UsePoisonQueue;

        result.Should().BeTrue();
    }

    [Fact]
    public async Task remove_message_async_should_call_poison_queue_item_repository_add_async()
    {
        var outboxMessageRepository = Substitute.For<IOutboxMessageRepository>();
        var poisonQueueItemRepository = Substitute.For<IPoisonQueueItemRepository>();
        var clock = Substitute.For<IClock>();
        clock.CurrentDate().Returns(CurrentDate);
        var moveOutboxMessageToPoisonQueueStrategy = new MoveOutboxMessageToPoisonQueueStrategy(outboxMessageRepository, poisonQueueItemRepository, clock);
        var message = new OutboxMessage("Outbox.Tests.Unit.Shared.Events.OrderCreated",
            "{\"Id\":\"814b3ef3-8fcc-4af6-bf5d-9f6562b886de\",\"Number\":\"test-1\",\"CreatedAt\":\"2024-03-19T17:41:11.2140516+01:00\",\"IsConfirmed\":false,\"ConfirmedAt\":null,\"CustomerEmail\":\"johndoe@exmail.com\"",
            DateTime.UtcNow);

        await moveOutboxMessageToPoisonQueueStrategy.RemoveMessageAsync(message);

        await outboxMessageRepository.Received(1).DeleteAsync(message);
        await poisonQueueItemRepository.Received(1).AddAsync(Arg.Is<PoisonQueueItem>(x =>
            x.Type == message.Type &&
            x.Data == message.Data &&
            x.CreatedAt == CurrentDate));
    }

    private static DateTime CurrentDate = new DateTime(2017, 2, 1, 12, 0, 0);
}
