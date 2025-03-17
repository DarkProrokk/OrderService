using Application.Commands;
using MediatR;

namespace Application.Handlers;

public class DeclineOrderCommandHandler( ) : IRequestHandler<DeclineOrderCommand>
{
    public async Task Handle(DeclineOrderCommand request, CancellationToken cancellationToken)
    {
       
    }
}