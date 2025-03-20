using Domain.Entity;

namespace Domain.Interfaces;

public interface IOrderFactory
{
    Task<Order> CreateAsync(Guid userGuid, List<Guid> productList);
}