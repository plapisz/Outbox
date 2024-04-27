using Bogus;
using Outbox.Tests.Shared.Types;

namespace Outbox.Tests.Shared.Builders;

public class OrderBuilder
{
    private readonly Faker _faker = new Faker();
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

    public OrderBuilder WithRandomId()
    {
        _id = _faker.Random.Guid();

        return this;
    }

    public OrderBuilder WithNumber(string number)
    {
        _number = number;

        return this;
    }

    public OrderBuilder WithRandomNumber()
    {
        _number = _faker.Random.String2(8);

        return this;
    }

    public OrderBuilder WithCreatedAt(DateTime createdAt)
    {
        _createdAt = createdAt;

        return this;
    }

    public OrderBuilder WithRandomCreatedAt()
    {
        _createdAt = _faker.Date.Past();

        return this;
    }

    public OrderBuilder WithCustomerEmail(string customerEmail)
    {
        _customerEmail = customerEmail;

        return this;
    }

    public OrderBuilder WithRandomCustomerEmail()
    {
        _customerEmail = _faker.Person.Email;

        return this;
    }

    public Order Create()
    {
        return new Order(_id, _number, _createdAt, _customerEmail);
    }
}
