using Outbox;
using Outbox.PostgreSql;
using Outbox.PostgreSql.Options;
using Outbox.Samples.Api.Events;
using Outbox.Samples.Api.Events.Handlers;
using Outbox.Samples.Api.Repositories;
using Outbox.Samples.Api.Services;
using Outbox.Samples.Api.Time;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration["postgres:connectionString"];
builder.Services
    .AddOutbox(cfg => cfg.PostgreSql(new PostgreSqlOptions() { ConnectionString = connectionString }))
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
