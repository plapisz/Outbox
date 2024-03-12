using Outbox.Events.Handlers;
using Outbox.Samples.Api.Services;

namespace Outbox.Samples.Api.Events.Handlers;

internal sealed class OrderCreatedHandler : IOutboxEventHandler<OrderCreated>
{
    private readonly IMailSender _mailSender;

    public OrderCreatedHandler(IMailSender mailSender)
    {
        _mailSender = mailSender;
    }

    public async Task HandleAsync(OrderCreated @event)
    {
        var receiver = @event.CustomerEmail;
        var subject = $"Order {@event.OrderNumber} created.";
        var body = $"Dear {@event.CustomerEmail}\nYour order no. {@event.OrderNumber} was created at {@event.OrderCreationDate}";

        await _mailSender.SendAsync(receiver, subject, body);
    }
}
