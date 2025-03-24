using Infrastructure.Entity;
using Messages;

namespace Infrastructure.Mappers;

public static class Mapper
{
    public static Order DomainOrderToInfOrder(Domain.Entity.Order entity)
    {
        return new Order
        {
            Guid = entity.Guid,
            UserId = entity.UserId,
            Number = entity.Number
        };
    }

    public static OrderCreatedEvent OrderToEvent(Domain.Entity.Order entity)
    {
        return new OrderCreatedEvent
        {
            OrderReference = entity.Guid,
            ItemsReference = entity.Products
        };
    }
}