using MediatR;

namespace Application.Commands;

public record CancelOrderCommand(Guid OrderGuid): IRequest;