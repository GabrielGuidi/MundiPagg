using MundiPagg.Infra.Shared.Interfaces;
using RabbitMQ.Client;
using System;
using System.Text;

namespace MundiPagg.Infra.Shared
{
    public class MessageBroker : IMessageBroker
    {
        private const string hostname = "rabbitmq";
        private const string username = "rabbitmq";
        private const string password = "rabbitmq";

        private readonly ConnectionFactory _connectionFactory;

        public MessageBroker()
        {
            _connectionFactory = CreateConnectionFactory();
        }

        private ConnectionFactory CreateConnectionFactory()
        {
            try
            {
                return new ConnectionFactory() { HostName = hostname, UserName = username, Password = password };
            }
            catch (Exception error)
            {
                throw new ApplicationException($"Fail connecting broker: {error.Message}");
            }
        }

        public void SendMessage(string message, string exchange, string routingKey)
        {
            using var connection = _connectionFactory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.ExchangeDeclare(exchange: exchange,
                                    type: ExchangeType.Topic);

            var body = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish(exchange: exchange,
                                 routingKey: routingKey,
                                 basicProperties: null,
                                 body: body);
        }
    }
}
