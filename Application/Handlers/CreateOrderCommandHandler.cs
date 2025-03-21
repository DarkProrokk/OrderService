using Application.Commands;
using Domain.Entity;
using Domain.Interfaces;
using Domain.Repository;
using MediatR;

namespace Application.Handlers;

public class CreateOrderCommandHandler(IOrderRepository orderRepository,
    IUnitOfWork unitOfWork, 
    IOrderFactory orderFactory): IRequestHandler<CreateOrderCommand, Guid>
{
    public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await orderFactory.CreateAsync(request.UserGuid, request.ProductsList.Keys.ToList());
        await orderRepository.AddAsync(order);
        await unitOfWork.SaveAsync();
        return order.Guid;
    }
}