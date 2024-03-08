using Outbox;
using Outbox.Samples.Api.Repositories;
using Outbox.Samples.Api.Time;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddOutbox()
    .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly))
    .AddSingleton<IOrderRepository, OrderRepository>()
    .AddSingleton<IClock, UtcClock>()
    .AddControllers();

var app = builder.Build();
app.MapControllers();
app.Run();
