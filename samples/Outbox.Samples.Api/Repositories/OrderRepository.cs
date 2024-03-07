using Outbox.Samples.Api.Domain;

namespace Outbox.Samples.Api.Repositories;

public sealed class OrderRepository : IOrderRepository
{
    private readonly List<Order> orders = new();

    public Task AddAsync(Order order)
    {
        orders.Add(order);
        return Task.CompletedTask;
    }

    public Task<Order> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Order order)
    {
        throw new NotImplementedException();
    }
}
