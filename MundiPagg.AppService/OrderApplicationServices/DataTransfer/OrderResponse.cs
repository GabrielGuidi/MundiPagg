using MundiPagg.Domain.CreateOrders.Entities;
using System;

namespace MundiPagg.AppService.DataTransfer
{
    public class OrderResponse
    {
        public static explicit operator OrderResponse(Order order)
        {
            return new OrderResponse();
        }
    }
}
