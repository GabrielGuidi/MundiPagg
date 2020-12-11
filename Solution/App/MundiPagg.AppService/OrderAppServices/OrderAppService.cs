using MundiPagg.AppService.DataTransfer;
using MundiPagg.AppService.OrderApplicationServices.Interfaces;
using MundiPagg.AppService.Shared;
using MundiPagg.Domain.CreateOrders.Interfaces;
using MundiPagg.Domain.Orders.Entities.Orders;

namespace MundiPagg.AppService.OrderApplicationServices
{
    public class OrderAppService : IOrderAppService
    {
        private readonly IOrderService _orderService;

        public OrderAppService(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public OrderResponse GetOrder(long id)
        {
            Order order = _orderService.GetOrder(id);

            if (order is null)
                return null;

            return (OrderResponse)order;
        }

        public OrderResponse GetOrder(string code)
        {
            Order order = _orderService.GetOrder(code);

            if (order is null)
                return null;

            return (OrderResponse)order;
        }

        public OrderResponse CreateNewOrder(OrderRequest orderRequest)
        {
            var newOrder = TranslateOrder.FromRequest(orderRequest.Order, orderRequest.OrderContent, orderRequest.EventSimulate);

            var order = _orderService.CreateNewOrder(newOrder);

            return (OrderResponse)order;
        }
    }
}