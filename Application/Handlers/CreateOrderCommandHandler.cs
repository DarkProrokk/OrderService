using Application.Commands;
using Application.Interfaces;
using Domain.Entity;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Repository;
using MediatR;
using Results;

namespace Application.Handlers;

public class CreateOrderCommandHandler(IOrderRepository orderRepository,
    IUnitOfWork unitOfWork, 
    IOrderFactory orderFactory,
    IMessageBusService busService): IRequestHandler<CreateOrderCommand, OperationResult>
{
    public async Task<OperationResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var order = await orderFactory.CreateAsync(request.UserGuid, request.ProductsList);
            await ProcessOrderAsync(order);
            return OperationResult.Success();
        }
        catch (Exception e)
        {
            return OperationResult.Failure(e.Message);
        }
    }


    private async Task ProcessOrderAsync(Order order)
    {
        await orderRepository.AddAsync(order);
        await unitOfWork.SaveAsync();
        await busService.PublishOrderCreatedForProcessing(order);
    }
}