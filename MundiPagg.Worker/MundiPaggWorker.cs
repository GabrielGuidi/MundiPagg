using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MundiPagg.Worker
{
    public class MundiPaggWorker : RabbitMqClientBase, IHostedService
    {
        private const string ProcessedSuccessQueueName = "processed_success_order_queue";
        private const string ProcessedFailQueueName = "processed_fail_order_queue";

        private readonly ILogger<MundiPaggWorker> _logger;

        public MundiPaggWorker(ConnectionFactory connectionFactory, ILogger<MundiPaggWorker> logger) : base(connectionFactory, logger)
        {
            _logger = logger;
            var consumer = new AsyncEventingBasicConsumer(Channel);
            consumer.Received += OnEventReceived;
            Channel.BasicConsume(queue: ProcessedSuccessQueueName, autoAck: false, consumer: consumer);

            var consumer_dlq = new AsyncEventingBasicConsumer(Channel);
            consumer_dlq.Received += OnFailEventReceived;
            Channel.BasicConsume(queue: ProcessedFailQueueName, autoAck: false, consumer: consumer_dlq);
        }


        public virtual Task StartAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        public virtual Task StopAsync(CancellationToken cancellationToken)
        {
            Dispose();
            return Task.CompletedTask;
        }

        protected virtual async Task OnEventReceived(object sender, BasicDeliverEventArgs @event)
        {
            var body = @event.Body.ToArray();
            var props = @event.BasicProperties;

            var id = props.CorrelationId; /*TODO Usar*/
            try
            {
                var message = Encoding.UTF8.GetString(body);

                _logger.LogInformation($"Message received: {props.CorrelationId}.");
                _logger.LogDebug($"Log: {message}.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Fail to receive: {props.CorrelationId}.");
                _logger.LogError($"Error: {ex.Message}.");
            }
            finally
            {
                Channel.BasicAck(deliveryTag: @event.DeliveryTag, multiple: false);
                _logger.LogTrace($"Ending process.");
                
                await Task.FromResult(0);/*TODO ?*/
            }
        }

        protected virtual async Task OnFailEventReceived(object sender, BasicDeliverEventArgs @event)
        {
            var body = @event.Body.ToArray();
            var props = @event.BasicProperties;

            var id = props.CorrelationId; /*TODO Usar*/
            try
            {
                var message = Encoding.UTF8.GetString(body);

                _logger.LogInformation($"Message received: {props.CorrelationId}.");
                _logger.LogDebug($"Log: {message}.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Fail to receive: {props.CorrelationId}.");
                _logger.LogError($"Error: {ex.Message}.");
            }
            finally
            {
                Channel.BasicAck(deliveryTag: @event.DeliveryTag, multiple: false);
                _logger.LogTrace($"Ending process and foward to dlq.");

                await Task.FromResult(0);/*TODO ?*/
            }
        }
    }
}
