using System.Text.Json.Serialization;

namespace MundiPagg.Domain.CreateOrders.Entities
{
    public class Card
    {
        [JsonPropertyName("number")]
        public string Number { get; set; }

        [JsonPropertyName("holder_name")]
        public string HolderName { get; set; }

        [JsonPropertyName("exp_month")]
        public long ExpMonth { get; set; }

        [JsonPropertyName("exp_year")]
        public long ExpYear { get; set; }

        [JsonPropertyName("cvv")]
        public string CVV { get; set; }

        [JsonPropertyName("billing_address")]
        public Address BillingAddress { get; set; }
    }
}
