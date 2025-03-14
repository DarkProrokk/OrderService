using MediatR;

namespace Application.Commands;

public record CreateOrderCommand(Guid UserGuid, List<Guid> ProductsList): IRequest<Guid>;