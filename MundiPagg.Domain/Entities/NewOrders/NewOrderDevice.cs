using System.Text.Json.Serialization;


namespace MundiPagg.Domain.CreateOrders.Entities.NewOrders
{
    public class NewOrderDevice
    {
        [JsonPropertyName("platform")]
        public string Platform { get; set; }
    }
}
