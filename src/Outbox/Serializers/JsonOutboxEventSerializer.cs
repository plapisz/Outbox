using System.Text.Json;

namespace Outbox.Serializers;

internal sealed class JsonOutboxEventSerializer : IOutboxEventSerializer
{
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public string Serialize<T>(T value) => JsonSerializer.Serialize(value, SerializerOptions);
}
