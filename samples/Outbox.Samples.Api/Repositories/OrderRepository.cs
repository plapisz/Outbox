using Outbox.Samples.Api.Domain;

namespace Outbox.Samples.Api.Repositories;

public sealed class OrderRepository : IOrderRepository
{
    private readonly List<Order> _orders = [];

    public Task AddAsync(Order order)
    {
        _orders.Add(order);

        return Task.CompletedTask;
    }

    public Task<Order> GetAsync(Guid id)
    {
        var order = _orders.SingleOrDefault(o => o.Id == id);

        return Task.FromResult(order);
    }

    public Task UpdateAsync(Order order)
    {
        return Task.CompletedTask;
    }
}
