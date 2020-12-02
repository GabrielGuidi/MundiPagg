using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Consumer
{
    internal class ServerConsumer
    {
        private const string hostname = "localhost";
        private const string username = "rabbitmq";
        private const string password = "rabbitmq";

        private const string routingKey_newOrder = "order.new";
        private const string exchange_order = "order_ex";

        private const string exchange_order_response = "order_ex_response";
        private const string routingKey_OrderProcessFail = "process.fail";
        private const string routingKey_OrderProcessSuccess = "process.success";

        //private readonly ILogger<ServerConsumer> _logger;

        public ServerConsumer(/*ILogger<ServerConsumer> logger*/)
        {
            //_logger = logger;
        }

        public static void Consume()
        {
            var factory = new ConnectionFactory() { HostName = hostname, UserName = username, Password = password };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            
            channel.ExchangeDeclare(exchange: exchange_order, type: ExchangeType.Topic);
            
            var queueName = channel.QueueDeclare().QueueName;
            channel.QueueBind(queue: queueName,
                              exchange: exchange_order,
                              routingKey: routingKey_newOrder);

            Console.WriteLine(" [*] Waiting for messages.");

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var routingKey = routingKey_OrderProcessFail;
                var response = string.Empty;
                var body = ea.Body.ToArray();
                var props = ea.BasicProperties;

                var replyProps = channel.CreateBasicProperties();
                replyProps.CorrelationId = props.CorrelationId;

                try
                {
                    var message = Encoding.UTF8.GetString(body);
                    response = ProcessOrder(message);
                    routingKey = routingKey_OrderProcessSuccess;
                    Console.WriteLine("Sucesso");
                }
                catch (Exception error)
                {
                    Console.WriteLine("Erro");
                    response = error.Message;
                }
                finally
                {
                    channel.ExchangeDeclare(exchange: props.ReplyTo, type: ExchangeType.Topic);

                    var responseBytes = Encoding.UTF8.GetBytes(response);
                    channel.BasicPublish(exchange: props.ReplyTo,
                                         routingKey: routingKey,
                                         basicProperties: replyProps,
                                         body: responseBytes);

                    channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                    Console.WriteLine("Send.");
                }
            };
            
            channel.BasicConsume(queue: queueName,
                                 autoAck: true,
                                 consumer: consumer);

            Console.ReadLine();
        }

        private static string ProcessOrder(string message)
        {
            var number = new Random().Next(0, 9);

            if (number > 6)
                throw new Exception("Deu ruim, depois eu trato isso!");

            return number.ToString();
        }
    }
}
