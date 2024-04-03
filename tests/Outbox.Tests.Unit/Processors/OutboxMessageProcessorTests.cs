using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Outbox.Entities;
using Outbox.Events.Handlers;
using Outbox.Processors;
using Outbox.Repositories;
using Outbox.Serializers;
using Outbox.Tests.Unit.Shared.Events;
using Xunit;

namespace Outbox.Tests.Unit.Processors;

public class OutboxMessageProcessorTests
{
    [Fact]
    public async Task process_async_should_call_handler_and_delete_async()
    {
        var message = GetMessage();
        var orderCreated = GetOrderCreatedEvent();
        _outboxMessageRepository.GetAllToProcessAsync().Returns(new List<OutboxMessage> { message });
        _outboxEventSerializer.Deserialize(message.Data, typeof(OrderCreated))
            .Returns(orderCreated);
        
        await _outboxMessageProcessor.ProcessAsync();

        await _handler.Received(1).HandleAsync(Arg.Is<OrderCreated>(x =>
            x.OrderId == orderCreated.OrderId &&
            x.OrderNumber == orderCreated.OrderNumber &&
            x.OrderCreationDate == orderCreated.OrderCreationDate &&
            x.CustomerEmail == orderCreated.CustomerEmail));
        await _outboxMessageRepository.Received(1).DeleteAsync(message);
    }

    private readonly IOutboxMessageRepository _outboxMessageRepository;
    private readonly IOutboxEventSerializer _outboxEventSerializer;
    private readonly IOutboxEventHandler<OrderCreated> _handler;
    private readonly OutboxMessageProcessor _outboxMessageProcessor;

    public OutboxMessageProcessorTests()
    {
        _outboxMessageRepository = Substitute.For<IOutboxMessageRepository>();
        _outboxEventSerializer = Substitute.For<IOutboxEventSerializer>();
        _handler = Substitute.For<IOutboxEventHandler<OrderCreated>>();
        var services = new ServiceCollection();
        services.AddSingleton((_) => _handler);
        var serviceProvider = services.BuildServiceProvider();
        _outboxMessageProcessor = new OutboxMessageProcessor(_outboxMessageRepository, _outboxEventSerializer, serviceProvider);
    }

    private static OutboxMessage GetMessage()
        => new("Outbox.Tests.Unit.Shared.Events.OrderCreated",
            "{\"Id\":\"814b3ef3-8fcc-4af6-bf5d-9f6562b886de\",\"Number\":\"test-1\",\"CreatedAt\":\"2024-03-19T17:41:11.2140516+01:00\",\"IsConfirmed\":false,\"ConfirmedAt\":null,\"CustomerEmail\":\"johndoe@exmail.com\"",
            DateTime.UtcNow);

    private static OrderCreated GetOrderCreatedEvent()
     => new(Guid.Parse("814b3ef3-8fcc-4af6-bf5d-9f6562b886de"),
         "test-1",
         DateTime.Parse("2024-03-19T17:41:11.2140516+01:00"),
         "johndoe@exmail.com");
}
