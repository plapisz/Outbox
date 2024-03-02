using FluentAssertions;
using Outbox.Tests.Unit.Shared.Events;
using Outbox.Tests.Unit.Shared.Types;
using Xunit;

namespace Outbox.Tests.Unit.Types;

public class OutboxEventSource_AddOutboxEvent_Tests
{
    [Fact]
    public void add_outbox_event_should_add_event_to_outbox_events()
    {
        var order = CreateOrder();

        var result = order.OutboxEvents;

        result.Should().NotBeEmpty();
        result.Should().HaveCount(1);
        result.ElementAt(0).Should().BeOfType<OrderCreated>();
    }

    [Fact]
    public void add_outbox_event_should_add_event_to_outbox_events_in_correct_order()
    {
        var order = CreateOrder();
        order.Confirm(DateTime.Now);

        var result = order.OutboxEvents;

        result.Should().NotBeEmpty();
        result.Should().HaveCount(2);
        result.ElementAt(0).Should().BeOfType<OrderCreated>();
        result.ElementAt(1).Should().BeOfType<OrderConfirmed>();
    }

    private static Order CreateOrder()
        => new(Guid.NewGuid(), "test-1", DateTime.Now, "johndoe@exmail.com");
}
