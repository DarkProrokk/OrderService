using Entities;
using MediatR;
using Results;

namespace Application.Commands;

public record CancelOrderCommand(Guid OrderGuid): IRequest<OperationResult>;