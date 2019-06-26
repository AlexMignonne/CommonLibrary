using MediatR;

namespace CommonLibrary.Mediator
{
    public abstract class Command<TResponse>
        : Message, IRequest<TResponse>
    {
    }

    public abstract class Command 
        : Message, IRequest
    {
    }
}