using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Text.Json.Serialization;

namespace MundiPagg.Domain.Orders.Entities.Orders
{
    public class Order
    {
        public Order(Item[] items, Customer customer)
        {
            Items = items;
            Customer = customer;
            Currency = "BRL";
            Closed = false;
        }

        [BsonId]
        [JsonIgnore]
        public ObjectId InternalId { get; set; }

        [JsonPropertyName("job_id")]
        public long JobId { get; private set; }

        [JsonPropertyName("id")]
        public string OrderId { get; set; }

        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("amount")]
        public long Amount { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("closed")]
        public bool Closed { get; set; }

        [JsonPropertyName("items")]
        public Item[] Items { get; set; }

        [JsonPropertyName("customer")]
        public Customer Customer { get; set; }

        [JsonPropertyName("shipping")]
        public Shipping Shipping { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("created_at")]
        public string CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public string UpdatedAt { get; set; }

        [JsonPropertyName("charges")]
        public Charge[] Charges { get; set; }

        public void SetJobId(long id)
        {
            if (JobId > 0)
                throw new ApplicationException("JobId can be set only once!");

            JobId = id;
        }
    }
}
