using Consumer.Domain.Orders.Interfaces;

namespace Consumer.Domain.Orders.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderApi _orderApi;

        public OrderService(IOrderApi orderApi)
        {
            _orderApi = orderApi;
        }

        public string CreateOrder(string message)
        {
            var orderResponse = _orderApi.CreateOrder(message);

            return orderResponse;
        }
    }
}
