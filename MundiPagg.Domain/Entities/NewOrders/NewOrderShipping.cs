using MundiPagg.Domain.Orders.Entities.Orders;
using System.Text.Json.Serialization;

namespace MundiPagg.Domain.CreateOrders.Entities.NewOrders
{
    public class NewOrderShipping
    {
        [JsonPropertyName("amount")]
        public long Amount { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("address")]
        public Address Address { get; set; }

        public static explicit operator Shipping(NewOrderShipping newOrderShipping)
        {
            return new Shipping
            {
                Amount = newOrderShipping.Amount,
                Description = newOrderShipping.Description,
                Address = newOrderShipping.Address
            };
        }
    }
}
