using Microsoft.Extensions.Logging;
using MundiPagg.Domain.CreateOrders.Entities.NewOrders;
using MundiPagg.Domain.CreateOrders.Interfaces;
using MundiPagg.Domain.Orders.Entities.Orders;
using MundiPagg.Domain.Shared;

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
            var createOrderJson = JsonTransform.OrderToCreateOrder(newOrder);

            var order = SaveNewOrder(newOrder);

            _orderBroker.SendOrderMessage(createOrderJson, order.JobId.ToString());
            _logger.LogInformation($"Message send to broker.");
            return order;
        }

        public Order UpdateProcessedOrder(long id, Order order)
        {
            var _order = _orderRepository.GetOrder(id);
            if (_order is null)
            {
                _logger.LogError($"Could not update Order with JobId {id}: Order not found!");
                return null;
            }

            order.SetJobId(id);
            order.InternalId = _order.InternalId;
            _orderRepository.Update(order);

            return order;
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
        #endregion
    }
}
