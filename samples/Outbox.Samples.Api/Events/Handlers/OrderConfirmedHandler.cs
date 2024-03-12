using Outbox.Events.Handlers;
using Outbox.Samples.Api.Services;

namespace Outbox.Samples.Api.Events.Handlers;

internal sealed class OrderConfirmedHandler : IOutboxEventHandler<OrderConfirmed>
{
    private readonly IMailSender _mailSender;

    public OrderConfirmedHandler(IMailSender mailSender)
    {
        _mailSender = mailSender;
    }

    public async Task HandleAsync(OrderConfirmed @event)
    {
        var receiver = @event.CustomerEmail;
        var subject = $"Order {@event.OrderNumber} confirmed.";
        var body = $"Dear {@event.CustomerEmail}\nYour order no. {@event.OrderNumber} was confirmed at {@event.OrderConfirmationDate}";

        await _mailSender.SendAsync(receiver, subject, body);
    }
}
