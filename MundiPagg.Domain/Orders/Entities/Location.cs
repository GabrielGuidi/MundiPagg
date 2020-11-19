using System.Text.Json.Serialization;


namespace MundiPagg.Domain.CreateOrders.Entities
{
    public class Location
    {
        [JsonPropertyName("latitude")]
        public string Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public string Longitude { get; set; }
    }
}
