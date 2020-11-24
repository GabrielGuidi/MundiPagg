using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MundiPagg.Consumer
{
    public class Worker : RabbitMqClientBase, IHostedService
    {
        private const string exchange_order_response = "order_ex_response";
        private const string routingKey_OrderProcessFail = "processed.fail";
        private const string routingKey_OrderProcessSuccess = "processed.success";
        private const string RequestQueueName = "request_order_queue";
        
        private readonly ILogger<Worker> _logger;

        public Worker(ConnectionFactory connectionFactory, ILogger<Worker> logger) : base(connectionFactory, logger)
        {
            _logger = logger;
            var consumer = new AsyncEventingBasicConsumer(Channel);
            consumer.Received += OnEventReceived;
            Channel.BasicConsume(queue: RequestQueueName, autoAck: false, consumer: consumer);
        }


        public virtual Task StartAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        public virtual Task StopAsync(CancellationToken cancellationToken)
        {
            Dispose();
            return Task.CompletedTask;
        }

        protected virtual async Task OnEventReceived(object sender, BasicDeliverEventArgs @event)
        {
            var routingKey = routingKey_OrderProcessFail;
            var response = string.Empty;
            var body = @event.Body.ToArray();
            var props = @event.BasicProperties;

            var replyProps = Channel.CreateBasicProperties();
            replyProps.CorrelationId = props.CorrelationId;
            try
            {
                var message = Encoding.UTF8.GetString(body);
                response = ProcessOrder(message);
                routingKey = routingKey_OrderProcessSuccess;
                _logger.LogInformation($"Success processed: {props.CorrelationId}.");
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Fail to process the order: {props.CorrelationId}.");
                response = ex.Message;
            }
            finally
            {
                var responseBytes = Encoding.UTF8.GetBytes(response);
                Channel.BasicPublish(exchange: exchange_order_response,
                                     routingKey: routingKey,
                                     basicProperties: replyProps,
                                     body: responseBytes);

                Channel.BasicAck(deliveryTag: @event.DeliveryTag, multiple: false);
                _logger.LogInformation($"Send message: {props.CorrelationId}.");

                await Task.FromResult(responseBytes);
            }
        }

        //protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        //{
        //    var factory = new ConnectionFactory() { HostName = hostname, UserName = username, Password = password };
        //    using var connection = factory.CreateConnection();
        //    using var channel = connection.CreateModel();

        //    _logger.LogInformation($"Starting listening.");

        //    channel.ExchangeDeclare(exchange: exchange_order, type: ExchangeType.Topic);

        //    var queueName = channel.QueueDeclare().QueueName;
        //    channel.QueueBind(queue: queueName,
        //                      exchange: exchange_order,
        //                      routingKey: routingKey_newOrder);

        //    var consumer = new EventingBasicConsumer(channel);
        //    channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);

        //    consumer.Received += (model, ea) =>
        //    {
        //        var routingKey = routingKey_OrderProcessFail;
        //        var response = string.Empty;
        //        var body = ea.Body.ToArray();
        //        var props = ea.BasicProperties;

        //        var replyProps = channel.CreateBasicProperties();
        //        replyProps.CorrelationId = props.CorrelationId;

        //        try
        //        {
        //            var message = Encoding.UTF8.GetString(body);
        //            response = ProcessOrder(message);
        //            routingKey = routingKey_OrderProcessSuccess;
        //            _logger.LogInformation($"Success processed: {props.CorrelationId}.");
        //        }
        //        catch (Exception error)
        //        {
        //            _logger.LogInformation($"Fail to process the order: {props.CorrelationId}.");
        //            response = error.Message;
        //        }
        //        finally
        //        {
        //            channel.ExchangeDeclare(exchange: props.ReplyTo, type: ExchangeType.Topic);

        //            var responseBytes = Encoding.UTF8.GetBytes(response);
        //            channel.BasicPublish(exchange: props.ReplyTo,
        //                                 routingKey: routingKey,
        //                                 basicProperties: replyProps,
        //                                 body: responseBytes);

        //            channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
        //            _logger.LogInformation($"Send message: {props.CorrelationId}.");
        //        }
        //    };

        //    //while (true)
        //    //{

        //    //}
        //    await Task.FromCanceled<String>(stoppingToken);
        //}

        private string ProcessOrder(string message)
        {
            var number = new Random().Next(0, 9);

            if (number > 6)
                throw new Exception("Deu ruim, depois eu trato isso!");

            return number.ToString();
        }
    }
}
