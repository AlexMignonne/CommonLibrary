using MediatR;

namespace Common.Mediator
{
    public abstract class Event
        : Message, INotification
    {
    }
}