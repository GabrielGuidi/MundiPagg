using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MundiPagg.Domain.CreateOrders.Interfaces;
using MundiPagg.Domain.Orders.Entities.Orders;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace MundiPagg.Worker
{
    public class MundiPaggWorker : RabbitMqClientBase, IHostedService
    {
        private const string ProcessedSuccessQueueName = "processed_success_order_queue";
        private const string ProcessedFailQueueName = "processed_fail_order_queue";

        private readonly ILogger<MundiPaggWorker> _logger;
        private readonly IOrderService _orderService;

        public MundiPaggWorker(ConnectionFactory connectionFactory, ILogger<MundiPaggWorker> logger, IOrderService orderService) : base(connectionFactory, logger)
        {
            _logger = logger;
            var consumer = new AsyncEventingBasicConsumer(Channel);
            consumer.Received += OnEventReceived;
            Channel.BasicConsume(queue: ProcessedSuccessQueueName, autoAck: false, consumer: consumer);

            var consumer_dlq = new AsyncEventingBasicConsumer(Channel);
            consumer_dlq.Received += OnFailEventReceived;
            Channel.BasicConsume(queue: ProcessedFailQueueName, autoAck: false, consumer: consumer_dlq);
            _orderService = orderService;
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

            var id = props.CorrelationId;
            try
            {
                var message = Encoding.UTF8.GetString(body);
                _logger.LogInformation($"Message related to JobId {props.CorrelationId} received from {ProcessedSuccessQueueName}.");

                UpdateOrder(id, message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Fail in processing message (JobId {props.CorrelationId}) received from {ProcessedSuccessQueueName}.");
                _logger.LogError($"Error: {ex.Message}.");
            }
            finally
            {
                Channel.BasicAck(deliveryTag: @event.DeliveryTag, multiple: false);
                _logger.LogTrace($"Ending process.");

                await Task.CompletedTask;
            }
        }

        protected virtual async Task OnFailEventReceived(object sender, BasicDeliverEventArgs @event)
        {
            var props = @event.BasicProperties;

            try
            {
                _logger.LogError($"Message related to JobId {props.CorrelationId} received from {ProcessedFailQueueName}.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}.");
            }
            finally
            {
                Channel.BasicAck(deliveryTag: @event.DeliveryTag, multiple: false);
                _logger.LogTrace($"Ending process and foward to dlq.");

                await Task.CompletedTask;
            }
        }

        #region [Private methods]
        private void UpdateOrder(string id, string message)
        {
            var order = JsonSerializer.Deserialize<Order>(message);

            if (long.TryParse(id, out long jobId))
            {
                _orderService.UpdateProcessedOrder(jobId, order);
                return;
            }

            throw new ApplicationException("JobId is not in the correct format!");
        }
        #endregion
    }
}
