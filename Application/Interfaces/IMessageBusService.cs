using Domain.Entity;

namespace Application.Interfaces;

public interface IMessageBusService
{
    Task PublishOrderCreatedForProduct();
    Task PublishOrderCreatedForProcessing(Order order);
}