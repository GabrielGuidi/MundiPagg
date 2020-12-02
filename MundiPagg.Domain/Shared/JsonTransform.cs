using MundiPagg.Domain.CreateOrders.Entities.NewOrders;
using System;
using System.Text.Json;

namespace MundiPagg.Domain.Shared
{
    public class JsonTransform
    {
        public static string OrderToCreateOrder(NewOrder order)
        {
            if (order == null)
                throw new ArgumentException("Order can not be null!", nameof(order));

            var json = JsonSerializer.Serialize(order);

            return json;
        }
    }
}
