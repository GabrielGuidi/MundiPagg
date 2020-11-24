using MundiPagg.Infra.Shared.Interfaces;
using RabbitMQ.Client;
using System;
using System.Text;

namespace MundiPagg.Infra.Shared
{
    public class MessageBroker : IMessageBroker
    {
        private const string hostname = "localhost";
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

        public void SendMessage(string message, string exchange, string routingKey, string exchangeResponse = null, string id = null)
        {
            using var connection = _connectionFactory.CreateConnection();
            using var channel = connection.CreateModel();

            var props = channel.CreateBasicProperties();
            props.CorrelationId = id ?? string.Empty;
            props.ReplyTo = exchangeResponse ?? string.Empty;

            channel.ExchangeDeclare(exchange: exchange, 
                                    type: ExchangeType.Topic,
                                    durable: true,
                                    autoDelete: false);

            var body = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish(exchange: exchange,
                                 routingKey: routingKey,
                                 basicProperties: props,
                                 body: body);
        }
    }
}
