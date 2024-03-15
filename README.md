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

## Retry policies

TODO

### Poison queue

## Persistence

TODO

### Postgres

### MongoDB

### Redis

## Sources

https://www.kamilgrzybek.com/blog/posts/the-outbox-pattern

https://www.youtube.com/watch?v=ukFjKZPm5Ag