using RabbitMQ.Client;

namespace Infrastructure.Messaging
{
    public class ConnectionProvider : IConnectionProvider, IDisposable
    {
        private readonly IConnection _connection;
        public ConnectionProvider(IConnection connection)
        {
            _connection = connection;
        }

        public static async Task<ConnectionProvider> CreateAsync(
                            string connectionString)
        {
            ConnectionFactory factory = new ConnectionFactory
            {
                HostName = connectionString
            };

            var connection =
                await factory.CreateConnectionAsync();

            return new ConnectionProvider(connection);
        }

        public IConnection GetConnection()
        {
            return _connection;
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }
    }
}
