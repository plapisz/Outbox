using MediatR;
using Outbox.Samples.Api.Repositories;
using Outbox.Samples.Api.Time;

namespace Outbox.Samples.Api.Commands.Handlers;

internal sealed class ConfirmOrderHandler : IRequestHandler<ConfirmOrder>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IClock _clock;

    public ConfirmOrderHandler(IOrderRepository orderRepository, IClock clock)
    {
        _orderRepository = orderRepository;
        _clock = clock;
    }

    public async Task Handle(ConfirmOrder command, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetAsync(command.Id);

        order.Confirm(_clock.CurrentDate());

        await order.DispatchOutboxEvents();
    }
}
