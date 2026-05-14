using System.Globalization;
using System.Text;
using System.Text.Json;
using Infrastructure.Messaging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shared.Messages;

namespace Consumer.Worker
{
    public class ConsumerService : BackgroundService
    {
        private readonly ILogger<ConsumerService> _logger;
        private readonly IConnectionProvider _provider;

        public ConsumerService(ILogger<ConsumerService> logger, IConnectionProvider provider)
        {
            _logger = logger;
            _provider = provider;
        }

        protected override async Task<Task> ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                _logger.LogInformation($"Entered ExecuteAsync at {DateTime.Now.ToString(CultureInfo.InvariantCulture)}");

                while (!stoppingToken.IsCancellationRequested)
                {
                    using var channel = await _provider
                                        .GetConnection()
                                        .CreateChannelAsync();                    

                    await channel.QueueDeclareAsync(
                        queue: "order-queue",
                        durable: true,
                        exclusive: false,
                        autoDelete: false);

                    var consumer = new AsyncEventingBasicConsumer(channel);

                    consumer.ReceivedAsync += async (model, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        var json = Encoding.UTF8.GetString(body);

                        var order = JsonSerializer.Deserialize<OrderCreatedEvent>(json);

                        Console.WriteLine($"Order Received:");
                        Console.WriteLine($"Id: {order?.OrderId}");
                        Console.WriteLine($"Product: {order?.ProductName}");
                        Console.WriteLine($"Price: {order?.Price}");
                    };

                    await channel.BasicConsumeAsync(
                        queue: "order-queue",
                        autoAck: true,
                        consumer: consumer);

                    await Task.Delay(1000, stoppingToken);

                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Error occured: "+ ex.Message);
                Console.WriteLine(ex.Message);
            }

            return Task.CompletedTask;
        }
    }
}
