using Outbox;
using Outbox.Samples.Api.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddOutbox()
    .AddSingleton<IOrderRepository, OrderRepository>()
    .AddControllers();

var app = builder.Build();
app.MapControllers();
app.Run();
