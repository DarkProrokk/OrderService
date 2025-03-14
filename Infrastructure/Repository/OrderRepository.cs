using Domain.Entity;
using Domain.Repository;
using Infrastructure.Context;
using Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class OrderRepository(OrderContext context): IOrderRepository
{
    public async Task AddAsync(Order order)
    {
        var dbEntity = Mapper.Map(order);
        await context.Orders.AddAsync(dbEntity);
    }

    public async Task<Order> GetByGuid(Guid guid)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Order>> GetByUserGuid(Guid guid)
    {
        throw new NotImplementedException();
    }

    public async Task<Order> GetByNumber(string number)
    {
        throw new NotImplementedException();
    }

    public async Task<int> GetOrdersCountByUserId(Guid userGuid)
    {
        return await context.Orders.CountAsync(entity => entity.UserId == userGuid);
    }
}