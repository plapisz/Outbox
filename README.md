# Outbox

Outbox is simple library to support Outbox pattern in your system.

## Table of contents
* [Introduction](#Introduction)
* [Quick start](#Quick-start)
* [Retry policies](#Retry-policies)
* [Persistence](#Persistence)
* [Sources](#Sources)

## Introduction

Sometimes our systems need to communicate with external components like external service or mail server for example to sending email after placing an order. Unfortunately external component can be unavailable at moment processing a business operation. Outbox Pattern helps us provide atomicity of our business operation.

## Quick Start

To add an Outbox to your project, simply call the **AddOutbox()** method w Program.cs

```
var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddOutbox()
    .AddControllers();

var app = builder.Build();
app.MapControllers();
app.Run();
```

Then, using marker interface **IOutboxEvent**, we can mark event that we want to process using outbox pattern.

```
public sealed record OrderCreated(Guid OrderId, string OrderNumber, DateTime OrderCreationDate, string CustomerEmail) : IOutboxEvent;
```

Entity to be able to generating event processing via outbox it should inherit from base class **OutboxEventSource**. Class OutboxEventSource provides method **AddOutboxEvent** which add event to list which be processing via outbox

```
public sealed class Order : OutboxEventSource
{
    public Guid Id { get; }
    public string Number { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public string CustomerEmail { get; private set; }

    public Order(Guid id, string number, DateTime createdAt, string customerEmail)
    {
        Id = id;
        Number = number;
        CreatedAt = createdAt;
        CustomerEmail = customerEmail;

        AddOutboxEvent(new OrderCreated(Id, Number, CreatedAt, CustomerEmail));
    }
}
```

Events processing occurs after calling **DispatchOutboxEventsAsync** method from base class OutboxEventSource

```
public async Task Handle(CreateOrder command, CancellationToken cancellationToken)
{
    var order = new Order(command.Id, command.Number, _clock.CurrentDate(), command.CustomerEmail);

    await _orderRepository.AddAsync(order);

    await order.DispatchOutboxEventsAsync();
}
```

Handler which will be able to processing outbox event should implement generic interface **IOutboxEventHandler<TEvent>**

```
internal sealed class OrderCreatedHandler : IOutboxEventHandler<OrderCreated>
{
    private readonly IMailSender _mailSender;

    public OrderCreatedHandler(IMailSender mailSender)
    {
        _mailSender = mailSender;
    }

    public async Task HandleAsync(OrderCreated @event)
    {
        var receiver = @event.CustomerEmail;
        var subject = $"Order {@event.OrderNumber} created.";
        var body = $"Dear {@event.CustomerEmail}\nYour order no. {@event.OrderNumber} was created at {@event.OrderCreationDate}";

        await _mailSender.SendAsync(receiver, subject, body);
    }
}
```

Registration of event handler is possible using method **AddOutboxEventHandler**

```
builder.Services
    .AddOutbox()
    .AddOutboxEventHandler<OrderCreated, OrderCreatedHandler>()
    .AddControllers();
```

## Retry policies

By default messages are removed after both success and failed processing. In second case we can define retry policy of messages processing.
To add retry policy you need to call method **WithRetryPolicy** on configuration object passing instance of **RetryPolicyOptions**.


```
builder.Services
    .AddOutbox(cfg => cfg.WithRetryPolicy(new RetryPolicyOptions(10, NextRetryAttemptsModeOptions.Exponential, true)))
    .AddControllers();
```


### Options of retry policy

To instance of **RetryPolicyOptions** you need to pass three parameters:
* maxRetryCount - integer number of maximum count of retry attemps
* nextRetryAttemptsMode - enumeration accepting one of three values
    * NotSet - next retry attemps will be placed as soon as possible
    * Regular - next retry attemps will be placed after one minute,
    * Exponential = next retry attemps will be placed in exponential way, after 1, 2, 4, 8, 16, 32 and so on
* UsePoisonQueue - boolean value, if set to false message is removed when set to true message is moved to special table called PoisonQueue

## Persistence

By default events processed via outbox are stored in memory. That means that when our system crash we lose data. Fortunately Outbox support different types of storages.

### PostgreSql

To add PostgreSql storage you need to add reference to **Outbox.PostgreSql**. Then you need to call **PostgreSql** extension method on configuration object in AddOutbox passing **PostgreSqlOptions** with connection string. 

```
builder.Services
    .AddOutbox(cfg => cfg.PostgreSql(new PostgreSqlOptions() { ConnectionString = connectionString }))
    .AddControllers();
```

### MongoDB

### Redis

## Sources

https://www.kamilgrzybek.com/blog/posts/the-outbox-pattern

https://www.youtube.com/watch?v=ukFjKZPm5Ag