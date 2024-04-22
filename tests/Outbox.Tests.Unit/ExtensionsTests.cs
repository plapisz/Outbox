using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Outbox.Dispatchers;
using Outbox.Events.Handlers;
using Outbox.Processors;
using Outbox.Repositories;
using Outbox.Serializers;
using Outbox.Services;
using Outbox.Tests.Shared.Events;
using Outbox.Tests.Shared.Events.Handlers;
using Outbox.Time;
using Xunit;

namespace Outbox.Tests.Unit;

public class ExtensionsTests
{
    [Theory]
    [InlineData(typeof(IOutboxEventDispatcher), typeof(OutboxEventDispatcher))]
    [InlineData(typeof(IOutboxMessageProcessor), typeof(OutboxMessageProcessor))]
    [InlineData(typeof(IClock), typeof(Clock))]
    [InlineData(typeof(IOutboxEventSerializer), typeof(JsonOutboxEventSerializer))]
    [InlineData(typeof(IOutboxMessageRepository), typeof(InMemoryOutboxMessageRepository))]
    [InlineData(typeof(IHostedService), typeof(OutboxMessageService))]
    public void add_outbox_should_add_all_necessary_services(Type serviceType, Type implementationType)
    {
        _services.AddOutbox();

        var provider = _services.BuildServiceProvider();
        var service = provider.GetService(serviceType);
        service.Should().NotBeNull();
        service.Should().BeOfType(implementationType);
    }

    [Fact]
    public void add_outbox_event_handler_should_register_event_handler()
    {
        _services.AddOutboxEventHandler<OrderCreated, OrderCreatedHandler>();

        var provider = _services.BuildServiceProvider();
        var service = provider.GetService<IOutboxEventHandler<OrderCreated>>();
        service.Should().NotBeNull();
        service.Should().BeOfType<OrderCreatedHandler>();
    }

    private readonly IServiceCollection _services;

    public ExtensionsTests()
    {
        _services = new ServiceCollection();
    }
}
