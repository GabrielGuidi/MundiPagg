using System.Text.Json.Serialization;

namespace MundiPagg.Domain.CreateOrders.Entities
{
    public class Antifraud
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("clearsale")]
        public Clearsale Clearsale { get; set; }
    }

    public class Clearsale
    {
        [JsonPropertyName("custom_sla")]
        public string CustomSla { get; set; }
    }
}
