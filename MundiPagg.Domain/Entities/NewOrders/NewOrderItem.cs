using MundiPagg.Domain.Orders.Entities.Orders;
using MundiPagg.Domain.Shared;
using System;
using System.Text.Json.Serialization;


namespace MundiPagg.Domain.CreateOrders.Entities.NewOrders
{
    public class NewOrderItem
    {
        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("quantity")]
        public long Quantity { get; set; }

        [JsonPropertyName("amount")]
        public long Amount { get; set; }

        public static explicit operator Item(NewOrderItem newOrderItem)
        {
            var item = new Item
            {
                Description = newOrderItem.Description,
                Amount = newOrderItem.Amount,
                Quantity = newOrderItem.Quantity
            };

            item.SetCreatedAt(FormatOrder.ToStringDate(DateTime.Now));

            return item;
        }
    }
}
