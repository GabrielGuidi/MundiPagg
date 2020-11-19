using System.Text.Json.Serialization;

namespace MundiPagg.Domain.CreateOrders.Entities
{
    public class Address
    {
        [JsonPropertyName("line_1")]
        public string Line1 { get; set; }

        [JsonPropertyName("zip_code")]
        public string ZipCode { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("state")]
        public string State { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }
    }
}
