using System;
using System.Text.Json.Serialization;

namespace MundiPagg.Domain.CreateOrders.Entities
{


    public class Order
    {
        public Order()
        {
            Id = Guid.NewGuid();
        }

        [JsonIgnore]
        public Guid Id { get; private set; }

        [JsonPropertyName("items")]
        public Item[] Items { get; set; }

        [JsonPropertyName("customer")]
        public Customer Customer { get; set; }

        [JsonPropertyName("ip")]
        public string Ip { get; set; }

        [JsonPropertyName("location")]
        public Location Location { get; set; }

        [JsonPropertyName("shipping")]
        public Shipping Shipping { get; set; }

        [JsonPropertyName("antifraud")]
        public Antifraud Antifraud { get; set; }

        [JsonPropertyName("session_id")]
        public string SessionId { get; set; }

        [JsonPropertyName("device")]
        public Device Device { get; set; }

        [JsonPropertyName("payments")]
        public Payment[] Payments { get; set; }
    }
}
