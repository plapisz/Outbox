using Outbox.Samples.Api.Entities;

namespace Outbox.Samples.Api.Repositories;

public interface IOrderRepository
{
    Task<Order> GetAsync(Guid id);
    Task AddAsync(Order order);
    Task UpdateAsync(Order order);
}
