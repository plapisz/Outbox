using NSubstitute;
using Outbox.RetryPolicy.NextRetryAttemptsStrategies.Resolvers;
using Outbox.RetryPolicy.NextRetryAttemptsStrategies;
using Outbox.RetryPolicy.Options;
using Xunit;
using Outbox.RetryPolicy.RemoveMessageStrategies;
using Outbox.Time;
using Outbox.Repositories;
using Outbox.RetryPolicy.RemoveMessageStrategies.Resolvers;
using FluentAssertions;

namespace Outbox.Tests.Unit.RetryPolicy.RemoveMessageStrategies.Resolvers;

public class RemoveOutboxMessageStrategyResolverTests
{
    [Theory]
    [InlineData(true, typeof(MoveOutboxMessageToPoisonQueueStrategy))]
    [InlineData(false, typeof(RemoveOutboxMessageDirectlyStrategy))]
    public void resolve_should_returns_correct_remove_outbox_message_strategy(bool usePoisonQueue, Type removeOutboxMessageStrategy)
    {
        var outboxMessageRepository = Substitute.For<IOutboxMessageRepository>();
        var poisonQueueItemRepository = Substitute.For<IPoisonQueueItemRepository>();
        var clock = Substitute.For<IClock>();
        var moveOutboxMessageToPoisonQueueStrategy = new MoveOutboxMessageToPoisonQueueStrategy(outboxMessageRepository, poisonQueueItemRepository, clock);
        var removeOutboxMessageDirectlyStrategy = new RemoveOutboxMessageDirectlyStrategy(outboxMessageRepository);
        var removeOutboxMessageStrategies = new List<IRemoveOutboxMessageStrategy>()
        {
            moveOutboxMessageToPoisonQueueStrategy,
            removeOutboxMessageDirectlyStrategy,
        };
        var resolver = new RemoveOutboxMessageStrategyResolver(removeOutboxMessageStrategies);

        var result = resolver.Resolve(usePoisonQueue);

        result.Should().NotBeNull();
        result.Should().BeOfType(removeOutboxMessageStrategy);
    }
}
