using RabbitMQ.Client;

namespace Infrastructure.Messaging
{
    public interface IConnectionProvider
    {
        IConnection GetConnection();
    }
}
