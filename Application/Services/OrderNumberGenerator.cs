using Domain.Interfaces;
using Domain.Repository;

namespace Application.Services;

public class OrderNumberGenerator(IOrderRepository orderRepository): IOrderNumberGenerator
{
    public async Task<string> GenerateNumber(Guid userId)
    {
        var orderCount = await orderRepository.GetOrdersCountByUserId(userId);
        return $"{DateTime.Now.Year}-{orderCount + 1:D4}";
    }
}