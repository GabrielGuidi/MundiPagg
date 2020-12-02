using Microsoft.Extensions.Configuration;
using MundiPagg.Infra.Shared.Interfaces;
using RabbitMQ.Client;
using System;
using System.Text;

namespace MundiPagg.Infra.Shared
{
    public class MessageBroker : IMessageBroker
    {
        private readonly string _hostname;
        private readonly string _username;
        private readonly string _password;

        private readonly ConnectionFactory _connectionFactory;

        public MessageBroker(IConfiguration config)
        {
            var rabbitMQConfig = config.GetSection("RabbitMQ");
            _hostname = rabbitMQConfig.GetSection("Hostname").Value;
            _username = rabbitMQConfig.GetSection("Username").Value;
            _password = rabbitMQConfig.GetSection("Password").Value;
            
            _connectionFactory = CreateConnectionFactory();
        }

        private ConnectionFactory CreateConnectionFactory()
        {
            try
            {
                return new ConnectionFactory() { HostName = _hostname, UserName = _username, Password = _password };
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
