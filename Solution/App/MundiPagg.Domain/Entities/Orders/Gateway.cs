using System.Text.Json.Serialization;

namespace MundiPagg.Domain.Orders.Entities.Orders
{
    public class Gateway
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("errors")]
        public string[] Errors { get; set; }
    }
}
