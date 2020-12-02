using System.Text.Json.Serialization;

namespace MundiPagg.Domain.Orders.Entities.Orders
{
    public class Antifraud
    {
        [JsonPropertyName("provider_name")]
        public string ProviderName { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("return_code")]
        public string ReturnCode { get; set; }

        [JsonPropertyName("return_message")]
        public string ReturnMessage { get; set; }

        [JsonPropertyName("score")]
        public string Score { get; set; }
    }
}
