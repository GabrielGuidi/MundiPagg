using System.Text.Json.Serialization;

namespace MundiPagg.Domain.Orders.Entities.Orders
{
    public class Card
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("first_six_digits")]
        public string FirstSixDigits { get; set; }

        [JsonPropertyName("last_four_digits")]
        public string LastFourDigits { get; set; }

        [JsonPropertyName("brand")]
        public string Brand { get; set; }

        [JsonPropertyName("holder_name")]
        public string HolderName { get; set; }

        [JsonPropertyName("exp_month")]
        public long ExpMonth { get; set; }

        [JsonPropertyName("exp_year")]
        public long ExpYear { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("created_at")]
        public string CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public string UpdatedAt { get; set; }

        [JsonPropertyName("billing_address")]
        public Address BillingAddress { get; set; }
    }
}
