using Application.Commands;
using Orderseer.Common.Models;
using Results;

namespace Application.Interfaces;

public interface IProcessingServiceClient
{
    Task<OperationResult> CancelStatusAsync(OrderStatusChangeModel guid);
}