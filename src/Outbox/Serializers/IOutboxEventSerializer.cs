namespace Outbox.Serializers;

internal interface IOutboxEventSerializer
{
    string Serialize(object value, Type type);
    object Deserialize(string value, Type type);
}
