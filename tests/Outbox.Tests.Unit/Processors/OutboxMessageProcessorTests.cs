using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Outbox.Entities;
using Outbox.Events.Handlers;
using Outbox.Processors;
using Outbox.Repositories;
using Outbox.RetryPolicy.NextRetryAttemptsStrategies.Resolvers;
using Outbox.RetryPolicy.Options;
using Outbox.RetryPolicy.RemoveMessageStrategies.Resolvers;
using Outbox.Serializers;
using Outbox.Tests.Shared.Builders;
using Outbox.Tests.Shared.Events;
using Outbox.Tests.Shared.Factories;
using Outbox.Time;
using Xunit;

namespace Outbox.Tests.Unit.Processors;

public class OutboxMessageProcessorTests
{
    [Fact]
    public async Task process_async_should_call_handler_and_delete_async()
    {
        var orderCreated = new OrderCreatedBuilder()
            .WithOrderId(Guid.NewGuid())
            .WithOrderNumber("test-1")
            .WithOrderCreationDate(DateTime.Now)
            .WithCustomerEmail("johndoe@exmail.com")
            .Create();
        var message = OutboxMessageFactory.Create(orderCreated);
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
    private readonly IRemoveOutboxMessageStrategyResolver _removeOutboxMessageStrategyResolver;
    private readonly INextRetryAttemptsStrategyResolver _nextRetryAttemptsStrategyResolver;
    private readonly IClock _clock;
    private readonly IOutboxEventHandler<OrderCreated> _handler;
    private readonly OutboxMessageProcessor _outboxMessageProcessor;

    public OutboxMessageProcessorTests()
    {
        _outboxMessageRepository = Substitute.For<IOutboxMessageRepository>();
        _outboxEventSerializer = Substitute.For<IOutboxEventSerializer>();
        _removeOutboxMessageStrategyResolver = Substitute.For<IRemoveOutboxMessageStrategyResolver>();
        _nextRetryAttemptsStrategyResolver = Substitute.For<INextRetryAttemptsStrategyResolver>();
        _clock = Substitute.For<IClock>();
        _handler = Substitute.For<IOutboxEventHandler<OrderCreated>>();
        var services = new ServiceCollection();
        services.AddSingleton((_) => _handler);
        var serviceProvider = services.BuildServiceProvider();
        _outboxMessageProcessor = new OutboxMessageProcessor(_outboxMessageRepository, 
            _outboxEventSerializer,
            _removeOutboxMessageStrategyResolver,
            _nextRetryAttemptsStrategyResolver,
            _clock,
            serviceProvider,
            new RetryPolicyOptions());
    }
}
