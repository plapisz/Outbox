using FluentAssertions;
using NSubstitute;
using Outbox.RetryPolicy.NextRetryAttemptsStrategies;
using Outbox.RetryPolicy.Options;
using Outbox.Time;
using Xunit;

namespace Outbox.Tests.Unit.RetryPolicy.NextRetryAttemptsStrategies;

public class RegularNextRetryAttemptsStrategyTests
{
    [Fact]
    public void next_retry_attempts_mode_should_return_regular()
    {
        var clock = Substitute.For<IClock>();
        var regularNextRetryAttemptsStrategy = new RegularNextRetryAttemptsStrategy(clock);

        var result = regularNextRetryAttemptsStrategy.NextRetryAttemptsMode;

        result.Should().Be(NextRetryAttemptsModeOptions.Regular);
    }

    [Theory, MemberData(nameof(Cases))]
    public void get_next_attempt_at_should_returns_correct_value(DateTime? lastAttemptAt, uint attemptsCount, DateTime? nextAttemptAt)
    {
        var clock = Substitute.For<IClock>();
        clock.CurrentDate().Returns(CurrentDate);
        var regularNextRetryAttemptsStrategy = new RegularNextRetryAttemptsStrategy(clock);

        var result = regularNextRetryAttemptsStrategy.GetNextAttemptAt(lastAttemptAt, attemptsCount);

        result.Should().Be(nextAttemptAt);
    }

    private static DateTime CurrentDate = new DateTime(2017, 2, 1, 12, 0, 0);

    public static TheoryData<DateTime?, uint, DateTime?> Cases = new()
    {
        { null, 0, new DateTime(2017, 2, 1, 12, 1, 0) },
        { null, 1, new DateTime(2017, 2, 1, 12, 1, 0) },
        { null, 2, new DateTime(2017, 2, 1, 12, 1, 0) },
        { null, 3, new DateTime(2017, 2, 1, 12, 1, 0) },
        { null, 4, new DateTime(2017, 2, 1, 12, 1, 0) },
        { null, 5, new DateTime(2017, 2, 1, 12, 1, 0) },
        { new DateTime(2017, 2, 1, 12, 0, 0), 0, new DateTime(2017, 2, 1, 12, 1, 0) },
        { new DateTime(2017, 2, 1, 12, 0, 0), 1, new DateTime(2017, 2, 1, 12, 1, 0) },
        { new DateTime(2017, 2, 1, 12, 0, 0), 2, new DateTime(2017, 2, 1, 12, 1, 0) },
        { new DateTime(2017, 2, 1, 12, 0, 0), 3, new DateTime(2017, 2, 1, 12, 1, 0) },
        { new DateTime(2017, 2, 1, 12, 0, 0), 4, new DateTime(2017, 2, 1, 12, 1, 0) },
        { new DateTime(2017, 2, 1, 12, 0, 0), 5, new DateTime(2017, 2, 1, 12, 1, 0) },
    };
}
