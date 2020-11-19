using MundiPagg.AppService.DataTransfer;
using MundiPagg.AppService.Models;
using MundiPagg.AppService.OrderApplicationServices.Interfaces;
using MundiPagg.Domain.CreateOrders.Entities;
using MundiPagg.Domain.CreateOrders.Interfaces;
using System.Text.Json;

namespace MundiPagg.AppService.OrderApplicationServices
{
    public class OrderAppService : IOrderAppService
    {
        private readonly IOrderService _orderService;

        public OrderAppService(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public OrderResponse CreateNewOrder(OrderRequest orderRequest)
        {
            var orderModel = JsonSerializer.Deserialize<OrderModel>(orderRequest.Order);
            var order = (Order)orderModel;

            Order response = _orderService.CreateNewOrder(order);
            
            return (OrderResponse)response;
        }
    }
}
