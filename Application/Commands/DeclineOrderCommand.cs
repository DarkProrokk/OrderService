using MediatR;

namespace Application.Commands;

public record DeclineOrderCommand(Guid OrderGuid): IRequest;