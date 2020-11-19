using System.Text.Json.Serialization;


namespace MundiPagg.Domain.CreateOrders.Entities
{
    public class Customer
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }
    }
}
