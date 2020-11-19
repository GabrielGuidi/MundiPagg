using System.Text.Json.Serialization;


namespace MundiPagg.Domain.CreateOrders.Entities
{
    public class Device
    {
        [JsonPropertyName("platform")]
        public string Platform { get; set; }
    }
}
