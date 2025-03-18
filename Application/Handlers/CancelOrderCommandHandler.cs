using Application.Commands;
using MediatR;

namespace Application.Handlers;

public class CancelOrderCommandHandler( ) : IRequestHandler<CancelOrderCommand>
{
    public async Task Handle(CancelOrderCommand request, CancellationToken cancellationToken)
    {
       
    }
}