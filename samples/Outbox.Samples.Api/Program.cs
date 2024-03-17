using Outbox;
using Outbox.Samples.Api.Events;
using Outbox.Samples.Api.Events.Handlers;
using Outbox.Samples.Api.Repositories;
using Outbox.Samples.Api.Services;
using Outbox.Samples.Api.Time;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddOutbox()
    .AddOutboxEventHandler<OrderCreated, OrderCreatedHandler>()
    .AddOutboxEventHandler<OrderConfirmed, OrderConfirmedHandler>()
    .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly))
    .AddSingleton<IOrderRepository, OrderRepository>()
    .AddSingleton<IClock, UtcClock>()
    .AddSingleton<IMailSender, DummyMailSender>()
    .AddControllers();

var app = builder.Build();
app.MapControllers();
app.Run();