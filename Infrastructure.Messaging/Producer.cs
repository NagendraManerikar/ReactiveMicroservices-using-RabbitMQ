using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Infrastructure.Messaging
{
    public class Producer : IProducer
    {
        private readonly IConnectionProvider _connectionProvider;

        public Producer(IConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }
        public async Task PublishAsync<T>(T message)
        {
            using var channel =
            await _connectionProvider
                .GetConnection()
                .CreateChannelAsync();

            await channel.QueueDeclareAsync(
                queue: "order-queue",
                durable: true,
                exclusive: false,
                autoDelete: false);

            var json = JsonSerializer.Serialize(message);

            var body = Encoding.UTF8.GetBytes(json);

            await channel.BasicPublishAsync(
                exchange: "",
                routingKey: "order-queue",
                body: body);

            Console.WriteLine("Message Published");
        }
    }
}
