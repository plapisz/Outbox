using System.Text;
using System.Text.Json;

namespace Outbox.Serializers;

internal sealed class JsonOutboxEventSerializer : IOutboxEventSerializer
{
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public string Serialize(object value, Type type) => JsonSerializer.Serialize(value, type, SerializerOptions);

    public object Deserialize(string value, Type type) => JsonSerializer.Deserialize(value, type, SerializerOptions);
}
