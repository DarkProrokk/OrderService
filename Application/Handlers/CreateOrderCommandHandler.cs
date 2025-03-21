using Application.Commands;
using Domain.Entity;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Repository;
using MediatR;
using Results;

namespace Application.Handlers;

public class CreateOrderCommandHandler(IOrderRepository orderRepository,
    IUnitOfWork unitOfWork, 
    IOrderFactory orderFactory): IRequestHandler<CreateOrderCommand, OperationResult>
{
    public async Task<OperationResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var order = await orderFactory.CreateAsync(request.UserGuid, request.ProductsList.Keys.ToList());
            await orderRepository.AddAsync(order);
            await unitOfWork.SaveAsync();
            return OperationResult.Success();
        }
        catch (Exception e)
        {
            return OperationResult.Failure(e.Message);
        }
    }
}