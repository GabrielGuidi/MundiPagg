using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;

namespace MundiPagg.Consumer
{
    public abstract class RabbitMqClientBase : IDisposable
    {
        private const string exchange_order = "order_ex";
        private const string exchange_order_response = "order_ex_response";
        private const string routingKey_newOrder = "order.new";
        private const string RequestQueueName = "request_order_queue";
        private const string ProcessedSuccessQueueName = "processed_success_order_queue";
        private const string ProcessedFailQueueName = "processed_fail_order_queue";
        private const string routingKey_OrderProcessFail = "processed.fail";
        private const string routingKey_OrderProcessSuccess = "processed.success";

        protected IModel Channel { get; private set; }
        private IConnection _connection;
        private readonly ConnectionFactory _connectionFactory;
        private readonly ILogger<RabbitMqClientBase> _logger;

        protected RabbitMqClientBase(
            ConnectionFactory connectionFactory,
            ILogger<RabbitMqClientBase> logger)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;
            ConnectToRabbitMq();
        }

        private void ConnectToRabbitMq()
        {
            if (_connection == null || _connection.IsOpen == false)
            {
                _connection = _connectionFactory.CreateConnection();
            }

            _logger.LogInformation("RabbitMQ connected.");

            if (Channel == null || Channel.IsOpen == false)
            {
                Channel = _connection.CreateModel();
                Channel.ExchangeDeclare(
                    exchange: exchange_order,
                    type: ExchangeType.Topic,
                    durable: true,
                    autoDelete: false);

                Channel.QueueDeclare(
                    queue: RequestQueueName,
                    durable: false,
                    exclusive: false,
                    autoDelete: false);

                Channel.QueueBind(
                    queue: RequestQueueName,
                    exchange: exchange_order,
                    routingKey: routingKey_newOrder);

                Channel.ExchangeDeclare(
                    exchange: exchange_order_response, 
                    type: ExchangeType.Topic,
                    durable: true,
                    autoDelete: false);

                Channel.QueueDeclare(
                    queue: ProcessedSuccessQueueName,
                    durable: false,
                    exclusive: false,
                    autoDelete: false);

                Channel.QueueBind(
                    queue: ProcessedSuccessQueueName,
                    exchange: exchange_order_response,
                    routingKey: routingKey_OrderProcessSuccess);

                Channel.QueueDeclare(
                    queue: ProcessedFailQueueName,
                    durable: false,
                    exclusive: false,
                    autoDelete: false);

                Channel.QueueBind(
                    queue: ProcessedFailQueueName,
                    exchange: exchange_order_response,
                    routingKey: routingKey_OrderProcessFail);
            }
        }

        public void Dispose()
        {
            try
            {
                Channel?.Close();
                Channel?.Dispose();
                Channel = null;

                _connection?.Close();
                _connection?.Dispose();
                _connection = null;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Cannot dispose RabbitMQ channel or connection");
            }
        }
    }
}
