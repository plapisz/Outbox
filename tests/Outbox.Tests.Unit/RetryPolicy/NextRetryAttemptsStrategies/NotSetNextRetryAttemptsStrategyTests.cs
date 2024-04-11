using FluentAssertions;
using Outbox.RetryPolicy.NextRetryAttemptsStrategies;
using Outbox.RetryPolicy.Options;
using Xunit;

namespace Outbox.Tests.Unit.RetryPolicy.NextRetryAttemptsStrategies;

public class NotSetNextRetryAttemptsStrategyTests
{
    [Fact]
    public void next_retry_attempts_mode_should_return_not_set()
    {
        var notSetNextRetryAttemptsStrategy = new NotSetNextRetryAttemptsStrategy();

        var result = notSetNextRetryAttemptsStrategy.NextRetryAttemptsMode;

        result.Should().Be(NextRetryAttemptsModeOptions.NotSet);
    }

    [Theory, MemberData(nameof(Cases))]
    public void get_next_attempt_at_should_returns_correct_value(DateTime? lastAttemptAt, uint attemptsCount, DateTime? nextAttemptAt)
    {
        var notSetNextRetryAttemptsStrategy = new NotSetNextRetryAttemptsStrategy();

        var result = notSetNextRetryAttemptsStrategy.GetNextAttemptAt(lastAttemptAt, attemptsCount);

        result.Should().Be(nextAttemptAt);
    }

    public static TheoryData<DateTime?, uint, DateTime?> Cases = new()
    {
        { null, 0, null },
        { null, 1, null },
        { null, 2, null },
        { null, 3, null },
        { null, 4, null },
        { null, 5, null },
        { new DateTime(2017, 2, 1, 12, 0, 0), 0, null },
        { new DateTime(2017, 2, 1, 12, 0, 0), 1, null },
        { new DateTime(2017, 2, 1, 12, 0, 0), 2, null },
        { new DateTime(2017, 2, 1, 12, 0, 0), 3, null },
        { new DateTime(2017, 2, 1, 12, 0, 0), 4, null },
        { new DateTime(2017, 2, 1, 12, 0, 0), 5, null },
    };
}
