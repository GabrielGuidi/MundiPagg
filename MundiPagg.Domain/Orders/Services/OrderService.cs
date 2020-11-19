using MundiPagg.Domain.CreateOrders.Entities;
using MundiPagg.Domain.CreateOrders.Interfaces;
using System;
using System.Text.Json;

namespace MundiPagg.Domain.CreateOrders.Services
{

    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderBroker _orderBroker;

        public OrderService(IOrderRepository orderRepository, IOrderBroker orderBroker)
        {
            _orderRepository = orderRepository;
            _orderBroker = orderBroker;
        }

        public Order CreateNewOrder(Order order)
        {
            var createOrderJson = TranslateToCreateOrderJson(order);

            _orderRepository.Insert(order);

            SendMessageByBroker(createOrderJson);

            return order;
        }

        private void SendMessageByBroker(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
                throw new ArgumentException("Json can not be null, empty or filler with white spaces!", "json");

            _orderBroker.SendOrderMessage(json);
        }

        private string TranslateToCreateOrderJson(Order order)
        {
            if (order == null)
                throw new ArgumentException("Order can not be null!", "json");

            var json = JsonSerializer.Serialize(order);

            return json;
        }
    }
}
