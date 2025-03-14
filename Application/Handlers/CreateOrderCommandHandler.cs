using Application.Commands;
using Domain.Entity;
using Domain.Interfaces;
using Domain.Repository;
using MediatR;

namespace Application.Handlers;

public class CreateOrderCommandHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork, IOrderNumberGenerator orderNumberGenerator): IRequestHandler<CreateOrderCommand, Guid>
{
    public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var number = await orderNumberGenerator.GenerateNumber(request.UserGuid);
        var order = new Order(request.UserGuid, request.ProductsList, number);
        await orderRepository.AddAsync(order);
        await unitOfWork.SaveAsync();
        return order.Guid;
    }
}