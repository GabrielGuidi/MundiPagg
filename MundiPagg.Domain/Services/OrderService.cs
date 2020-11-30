using Microsoft.Extensions.Logging;
using MundiPagg.Domain.CreateOrders.Entities.NewOrders;
using MundiPagg.Domain.CreateOrders.Interfaces;
using MundiPagg.Domain.Orders.Entities.Orders;
using System;
using System.Text.Json;

namespace MundiPagg.Domain.CreateOrders.Services
{

    public class OrderService : IOrderService
    {
        private readonly ILogger<OrderService> _logger;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderBroker _orderBroker;

        public OrderService(IOrderRepository orderRepository, IOrderBroker orderBroker, ILogger<OrderService> logger)
        {
            _orderRepository = orderRepository;
            _orderBroker = orderBroker;
            _logger = logger;
        }

        public Order GetOrder(long id)
        {
            var order = _orderRepository.GetOrder(id);

            return order;
        }

        public Order GetOrder(string code)
        {
            var order = _orderRepository.GetOrder(code);

            return order;
        }

        public Order CreateNewOrder(NewOrder newOrder)
        {
            var createOrderJson = TransformOrderToCreateOrderJson(newOrder);

            var order = SaveNewOrder(newOrder);

            SendMessageByBroker(createOrderJson, order.JobId.ToString());

            return order;
        }

        public void UpdateProcessedOrder(long id, Order order)
        {
            var _order = _orderRepository.GetOrder(id);
            if (_order is null)
            {
                _logger.LogError($"Could not update Order with JobId {id}: Order not found!");
                return;
            }

            order.SetJobId(id);
            order.InternalId = _order.InternalId;
            _orderRepository.Update(order);
        }

        #region [Private methods]
        private Order SaveNewOrder(NewOrder newOrder)
        {
            var nextId = _orderRepository.GetNextId();

            var order = (Order)newOrder;
            order.SetJobId(nextId);

            _orderRepository.Create(order);
            _logger.LogInformation($"Order {order.Code} create with JobId: {order.JobId}.");

            return order;
        }

        private void SendMessageByBroker(string json, string id)
        {
            if (string.IsNullOrWhiteSpace(json))
                throw new ArgumentException("Json can not be null, empty or filler with white spaces!", "json");

            _orderBroker.SendOrderMessage(json, id);
            _logger.LogInformation($"Message send to broker.");
        }

        private string TransformOrderToCreateOrderJson(NewOrder order)
        {
            if (order == null)
                throw new ArgumentException("Order can not be null!", "json");

            var json = JsonSerializer.Serialize(order);

            return json;
        }
        #endregion
    }
}
