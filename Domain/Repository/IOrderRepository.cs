using Domain.Entity;

namespace Domain.Repository;

public interface IOrderRepository
{
    Task AddAsync(Order order);
    
    Task<Order> GetByGuid(Guid guid);
    
    Task<IEnumerable<Order>> GetByUserGuid(Guid guid);

    Task<Order> GetByNumber(string number);

    Task<int> GetOrdersCountByUserId(Guid userGuid);
}