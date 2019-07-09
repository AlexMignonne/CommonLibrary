using System.Threading.Tasks;
using CommonLibrary.Messages;
using CommonLibrary.SeedOfWork;
using RabbitMQ.Client;

namespace CommonLibrary.RabbitMQ
{
    public interface IBusPublisher
    {
        Task Send<TCommand>(
            TCommand command,
            Enumeration commandType,
            string type,
            string routingKey,
            bool durable = true,
            bool autoDelete = false,
            IBasicProperties properties = null)
            where TCommand : Command;

        Task Publish<TEvent>(
            TEvent @event,
            Enumeration eventType,
            bool autoDelete = true,
            IBasicProperties properties = null)
            where TEvent : Event;
    }
}
