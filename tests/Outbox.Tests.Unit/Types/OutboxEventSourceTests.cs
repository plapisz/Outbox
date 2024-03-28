using FluentAssertions;
using Outbox.Tests.Unit.Shared.Events;
using Outbox.Tests.Unit.Shared.Types;
using Outbox.Types;
using Xunit;

namespace Outbox.Tests.Unit.Types;

public class OutboxEventSourceTests
{
    [Fact]
    public void add_outbox_event_should_add_event_to_outbox_events()
    {
        var order = CreateOrder();

        order.OutboxEvents.Should().NotBeEmpty();
        order.OutboxEvents.Should().HaveCount(1);
        order.OutboxEvents.ElementAt(0).Should().BeOfType<OrderCreated>();
    }

    [Fact]
    public void add_outbox_event_should_add_event_to_outbox_events_in_correct_order()
    {
        var order = CreateOrder();
        order.Confirm(DateTime.Now);

        order.OutboxEvents.Should().NotBeEmpty();
        order.OutboxEvents.Should().HaveCount(2);
        order.OutboxEvents.ElementAt(0).Should().BeOfType<OrderCreated>();
        order.OutboxEvents.ElementAt(1).Should().BeOfType<OrderConfirmed>();
    }

    [Fact]
    public async Task dispatch_outbox_events_should_raise_outbox_events_dispatched_event()
    {
        var isEventRaied = false;
        OutboxEventSource.OutboxEventsDispatched += (OutboxEventSource sender) =>
        {
            isEventRaied |= true;
            return Task.CompletedTask;
        };
        var order = CreateOrder();
        order.Confirm(DateTime.Now);

        await order.DispatchOutboxEvents();

        isEventRaied.Should().BeTrue();
    }

    [Fact]
    public async Task dispatch_outbox_events_should_clear_events()
    {
        var order = CreateOrder();
        order.Confirm(DateTime.Now);

        await order.DispatchOutboxEvents();
        
        order.OutboxEvents.Should().BeEmpty();
    }

    private static Order CreateOrder()
        => new(Guid.NewGuid(), "test-1", DateTime.Now, "johndoe@exmail.com");
}
