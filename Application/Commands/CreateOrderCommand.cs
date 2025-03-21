using MediatR;
using Results;

namespace Application.Commands;

public record CreateOrderCommand(Guid UserGuid, Dictionary<Guid, int> ProductsList): IRequest<OperationResult>;