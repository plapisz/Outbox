using NSubstitute;
using Outbox.Dispatchers;
using Outbox.Entities;
using Outbox.Repositories;
using Outbox.Serializers;
using Outbox.Tests.Unit.Shared.Events;
using Outbox.Tests.Unit.Shared.Types;
using Outbox.Time;
using Xunit;

namespace Outbox.Tests.Unit.Dispatchers;

public class OutboxEventDispatcherTests
{
    [Fact]
    public async Task dispatch_outbox_event_async_should_call_outbox_message_repository_add_async()
    {
        var order = CreateOrder();
        var type = "Outbox.Tests.Unit.Shared.Events.OrderCreated";
        var json = "{\"Id\":\"814b3ef3-8fcc-4af6-bf5d-9f6562b886de\",\"Number\":\"test-1\",\"CreatedAt\":\"2024-03-19T17:41:11.2140516+01:00\",\"IsConfirmed\":false,\"ConfirmedAt\":null,\"CustomerEmail\":\"johndoe@exmail.com\",\"OutboxEvents\":[{}]}";
        var now = DateTime.UtcNow;
        _serializer.Serialize(Arg.Any<OrderCreated>(), typeof(OrderCreated)).Returns(json);
        _clock.CurrentDate().Returns(now);

        await _outboxEventDispatcher.DispatchOutboxEventAsync(order);

        await _outboxMessageRepository.Received(1).AddAsync(Arg.Is<OutboxMessage>(x =>
                x.Type == type && 
                x.Data == json && 
                x.CreatedAt == now));
    }

    private readonly IClock _clock;
    private readonly IOutboxEventSerializer _serializer;
    private readonly IOutboxMessageRepository _outboxMessageRepository;
    private readonly OutboxEventDispatcher _outboxEventDispatcher;

    public OutboxEventDispatcherTests()
    {
        _clock = Substitute.For<IClock>();
        _serializer = Substitute.For<IOutboxEventSerializer>();
        _outboxMessageRepository = Substitute.For<IOutboxMessageRepository>();
        _outboxEventDispatcher = new OutboxEventDispatcher(_clock, _serializer, _outboxMessageRepository);
    }
    private static Order CreateOrder()
        => new(Guid.NewGuid(), "test-1", DateTime.Now, "johndoe@exmail.com");
}
