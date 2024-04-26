using Outbox.Tests.Shared.Types;

namespace Outbox.Tests.Shared.Builders;

public class OrderBuilder
{
    private Guid _id;
    private string _number;
    private DateTime _createdAt;
    private string _customerEmail;

    public OrderBuilder()
    {

    }

    public OrderBuilder WithId(Guid id)
    {
        _id = id;

        return this;
    }

    public OrderBuilder WithNumber(string number)
    {
        _number = number;

        return this;
    }

    public OrderBuilder WithCreatedAt(DateTime createdAt)
    {
        _createdAt = createdAt;

        return this;
    }

    public OrderBuilder WithCustomerEmial(string customerEmail)
    {
        _customerEmail = customerEmail;

        return this;
    }

    public Order Create()
    {
        return new Order(_id, _number, _createdAt, _customerEmail);
    }
}
