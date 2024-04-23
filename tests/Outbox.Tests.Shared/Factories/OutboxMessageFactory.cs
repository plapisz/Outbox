using Outbox.Entities;
using Outbox.Events;
using System.Text.Json;

namespace Outbox.Tests.Shared.Factories;

public static class OutboxMessageFactory
{
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public static OutboxMessage Create<TEvent>(TEvent @event) where TEvent : class, IOutboxEvent
    {
        var type = @event.GetType();
        var data = JsonSerializer.Serialize(@event, type, SerializerOptions);

        return new OutboxMessage(type.FullName, data, DateTime.Now);
    }
}
