using Domain.Entity;

namespace Application.Interfaces;

public interface IMessageBusService
{
    Task PublishOrderCreated(Order order);
}