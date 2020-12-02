using System.Text.Json.Serialization;


namespace MundiPagg.Domain.CreateOrders.Entities.NewOrders
{
    public class NewOrderLocation
    {
        [JsonPropertyName("latitude")]
        public string Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public string Longitude { get; set; }
    }
}
