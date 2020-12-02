using MundiPagg.Domain.Shared;
using System;
using System.Text.Json.Serialization;

namespace MundiPagg.Domain.Orders.Entities.Orders
{
    public class Item
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("amount")]
        public long Amount { get; set; }

        [JsonPropertyName("quantity")]
        public long Quantity { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("created_at")]
        public string CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public string UpdatedAt { get; set; }

        public void SetStatus(string status)
        {
            if (!ValidateOrder.IsValidItemStatus(status))
                throw new ApplicationException("Status must be active or deleted!");

            Status = status;
        }

        public void SetCreatedAt(string date)
        {
            if (!ValidateOrder.IsValidDate(date))
                throw new ApplicationException("Date is not in correct format!");

            CreatedAt = date;
        }

        public void SetUpdatedAt(string date)
        {
            if (!ValidateOrder.IsValidDate(date))
                throw new ApplicationException("Date is not in correct format!");

            UpdatedAt = date;
        }
    }
}
