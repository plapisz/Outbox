namespace Outbox.Serializers;

internal interface IOutboxEventSerializer
{
    string Serialize<T>(T value);
    object Deserialize(string value, Type type);
}
