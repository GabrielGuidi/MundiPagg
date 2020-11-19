using System.Text.Json.Serialization;

namespace MundiPagg.Domain.CreateOrders.Entities
{
    public class Shipping
    {
        [JsonPropertyName("amount")]
        public long Amount { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("recipient_name")]
        public string RecipientName { get; set; }

        [JsonPropertyName("recipient_phone")]
        public string RecipientPhone { get; set; }

        [JsonPropertyName("address")]
        public Address Address { get; set; }
    }
}
