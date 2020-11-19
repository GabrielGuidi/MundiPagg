using System.Text.Json.Serialization;


namespace MundiPagg.Domain.CreateOrders.Entities
{
    public class Item
    {
        [JsonPropertyName("amount")]
        public long Amount { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("quantity")]
        public long Quantity { get; set; }
    }
}
