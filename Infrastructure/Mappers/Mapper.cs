using Infrastructure.Entity;
using KafkaMessages;

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

    public static OrderCreatedForProcessingEvent OrderToEvent(Domain.Entity.Order entity)
    {
        return new OrderCreatedForProcessingEvent
        {
            OrderReference = entity.Guid
        };
    }
}