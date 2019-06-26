using MediatR;

namespace CommonLibrary.Mediator
{
    public abstract class Event
        : Message, INotification
    {
    }
}