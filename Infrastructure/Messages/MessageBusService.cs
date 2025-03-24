using System.Text.Json;
using Application.Interfaces;
using Domain.Entity;
using Domain.Interfaces;
using Infrastructure.Mappers;
using Messages;

namespace Infrastructure.Messages;

public class MessageBusService(IBus bus): IMessageBusService
{
    public async Task PublishOrderCreatedForProduct()
    {
        throw new NotImplementedException();
    }

    public async Task PublishOrderCreatedForProcessing(Order order)
    {
        var message = Mapper.OrderToEvent(order);
        var msg = JsonSerializer.Serialize(message);
        await bus.PublishAsync("order_created", message.OrderReference.ToString(), msg);
    }
}