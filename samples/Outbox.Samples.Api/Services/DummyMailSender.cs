namespace Outbox.Samples.Api.Services;

public sealed class DummyMailSender : IMailSender
{
    public Task SendAsync(string receiver, string subject, string body)
    {
        Console.WriteLine($"Sending mail\nTo: {receiver}\nSubject: {subject}\nBody: {body}");

        return Task.CompletedTask;
    }
}
