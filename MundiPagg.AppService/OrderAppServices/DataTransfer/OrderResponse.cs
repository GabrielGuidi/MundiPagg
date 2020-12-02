using MundiPagg.Domain.Orders.Entities.Orders;
using System.Text.Json;

namespace MundiPagg.AppService.DataTransfer
{
    public class OrderResponse
    {
        public long JobId { get; set; }
        public string Order { get; set; }

        public static explicit operator OrderResponse(Order order)
        {
            return new OrderResponse()
            {
                JobId = order.JobId,
                Order = JsonSerializer.Serialize(order)
            };
        }
    }
}
