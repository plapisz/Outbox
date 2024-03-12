namespace Outbox.Samples.Api.Services;

public interface IMailSender
{
    Task SendAsync(string receiver, string subject, string body);
}
