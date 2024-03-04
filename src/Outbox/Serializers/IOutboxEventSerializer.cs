namespace Outbox.Serializers;

internal interface IOutboxEventSerializer
{
    public string Serialize<T>(T value);
}
