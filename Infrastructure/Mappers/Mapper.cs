using Infrastructure.Entity;

namespace Infrastructure.Mappers;

public static class Mapper
{
    public static Order Map(Domain.Entity.Order entity)
    {
        return new Order
        {
            Guid = entity.Guid,
            UserId = entity.UserId,
            Number = entity.Number
        };
    }
}