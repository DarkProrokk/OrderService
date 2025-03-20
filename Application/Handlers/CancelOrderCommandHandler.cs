using Application.Commands;
using Application.Interfaces;
using Entities;
using MediatR;
using Orderseer.Common.Models;
using Results;

namespace Application.Handlers;

public class CancelOrderCommandHandler(IProcessingServiceClient processingServiceClient): IRequestHandler<CancelOrderCommand, OperationResult>
{
    public async Task<OperationResult> Handle(CancelOrderCommand command, CancellationToken cancellationToken)
    {
        var request = new OrderStatusChangeModel(command.OrderGuid, OrderStatus.Cancelled);
        var result = await processingServiceClient.CancelStatusAsync(request);
        return result;
    }
}