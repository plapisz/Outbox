using FluentAssertions;
using NSubstitute;
using Outbox.RetryPolicy.NextRetryAttemptsStrategies;
using Outbox.RetryPolicy.NextRetryAttemptsStrategies.Resolvers;
using Outbox.RetryPolicy.Options;
using Outbox.Time;
using Xunit;

namespace Outbox.Tests.Unit.RetryPolicy.NextRetryAttemptsStrategies.Resolvers;

public class NextRetryAttemptsStrategyResolverTests
{
    [Theory]
    [InlineData(NextRetryAttemptsModeOptions.NotSet, typeof(NotSetNextRetryAttemptsStrategy))]
    [InlineData(NextRetryAttemptsModeOptions.Regular, typeof(RegularNextRetryAttemptsStrategy))]
    [InlineData(NextRetryAttemptsModeOptions.Exponential, typeof(ExponentialNextRetryAttemptsStrategy))]
    public void resolve_should_returns_correct_next_retry_attempts_strategy(NextRetryAttemptsModeOptions nextRetryAttemptsMode, Type nextRetryAttemptsStrategy)
    {
        var clock = Substitute.For<IClock>();
        var notSetNextRetryAttemptsStrategy = new NotSetNextRetryAttemptsStrategy();
        var regularNextRetryAttemptsStrategy = new RegularNextRetryAttemptsStrategy(clock);
        var exponentialNextRetryAttemptsStrategy = new ExponentialNextRetryAttemptsStrategy(clock);
        var nextRetryAttemptsStrategies = new List<INextRetryAttemptsStrategy>()
        {
            notSetNextRetryAttemptsStrategy,
            regularNextRetryAttemptsStrategy,
            exponentialNextRetryAttemptsStrategy,
        };
        var resolver = new NextRetryAttemptsStrategyResolver(nextRetryAttemptsStrategies);

        var result = resolver.Resolve(nextRetryAttemptsMode);

        result.Should().NotBeNull();
        result.Should().BeOfType(nextRetryAttemptsStrategy);
    }
}
