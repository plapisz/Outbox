using Bogus;
using Outbox.Tests.Shared.Events;

namespace Outbox.Tests.Shared.Builders;

public class OrderCreatedBuilder
{
    private readonly Faker _faker = new Faker();
    private Guid _orderId;
    private string _orderNumber;
    private DateTime _orderCreationDate;
    private string _customerEmail;

    public OrderCreatedBuilder()
    {

    }

    public OrderCreatedBuilder WithOrderId(Guid orderId)
    {
        _orderId = orderId;

        return this;
    }

    public OrderCreatedBuilder WithRandomOrderId()
    {
        _orderId = _faker.Random.Guid();

        return this;
    }

    public OrderCreatedBuilder WithOrderNumber(string orderNumber)
    {
        _orderNumber = orderNumber;

        return this;
    }

    public OrderCreatedBuilder WithRandomOrderNumber()
    {
        _orderNumber = _faker.Random.String2(8);

        return this;
    }

    public OrderCreatedBuilder WithOrderCreationDate(DateTime orderCreationDate)
    {
        _orderCreationDate = orderCreationDate;

        return this;
    }

    public OrderCreatedBuilder WithRandomOrderCreationDate()
    {
        _orderCreationDate = _faker.Date.Past();

        return this;
    }

    public OrderCreatedBuilder WithCustomerEmail(string customerEmail)
    {
        _customerEmail = customerEmail;

        return this;
    }

    public OrderCreatedBuilder WithRandomCustomerEmail()
    {
        _customerEmail = _faker.Person.Email;

        return this;
    }

    public OrderCreated Create()
    {
        return new OrderCreated(_orderId, _orderNumber, _orderCreationDate, _customerEmail);
    }
}
