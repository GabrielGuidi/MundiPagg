using MundiPagg.Domain.CreateOrders.Interfaces;
using MundiPagg.Infra.Shared.Interfaces;
using System;

namespace MundiPagg.Infra.Orders.Brokers
{
    public class OrderBroker : IOrderBroker
    {
        private const string routingKey_newOrder = "order.new";
        private const string exchange_order = "order_ex";
        private const string exchange_order_response = "order_ex_response";

        private readonly IMessageBroker _messageBroker;

        public OrderBroker(IMessageBroker messageBroker)
        {
            _messageBroker = messageBroker;
        }

        public void SendOrderMessage(string json, string id)
        {
            if (string.IsNullOrWhiteSpace(json))
                throw new ArgumentException("Json can not be null, empty or filler with white spaces!", nameof(json));

            _messageBroker.SendMessage(json, exchange_order, routingKey_newOrder, exchange_order_response, id);
        }
    }
}
