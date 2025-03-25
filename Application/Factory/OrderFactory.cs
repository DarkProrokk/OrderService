using Domain.Entity;
using Domain.Interfaces;

namespace Application.Factory;

public class OrderFactory(IOrderNumberGenerator orderNumberGenerator): IOrderFactory
{
    public async Task<Order> CreateAsync(Guid userGuid, Dictionary<Guid, int> productList)
    {
        var number = await orderNumberGenerator.GenerateNumber(userGuid);
        return Order.Create(userGuid, productList, number);
    }
}