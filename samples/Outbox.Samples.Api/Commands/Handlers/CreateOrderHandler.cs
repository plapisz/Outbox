using MediatR;
using Outbox.Samples.Api.Entities;
using Outbox.Samples.Api.Repositories;
using Outbox.Samples.Api.Time;

namespace Outbox.Samples.Api.Commands.Handlers;

internal sealed class CreateOrderHandler : IRequestHandler<CreateOrder>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IClock _clock;

    public CreateOrderHandler(IOrderRepository orderRepository, IClock clock)
    {
        _orderRepository = orderRepository;
        _clock = clock;
    }

    public async Task Handle(CreateOrder command, CancellationToken cancellationToken)
    {
        var order = new Order(command.Id, command.Number, _clock.CurrentDate(), command.CustomerEmail);

        await _orderRepository.AddAsync(order);

        order.DispatchOutboxEvents();
    }
}
