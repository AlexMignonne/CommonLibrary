using MediatR;

namespace Common.Mediator
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