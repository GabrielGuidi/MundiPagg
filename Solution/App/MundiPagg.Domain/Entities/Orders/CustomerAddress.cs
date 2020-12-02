using MundiPagg.Domain.Shared;
using System;
using System.Text.Json.Serialization;

namespace MundiPagg.Domain.Orders.Entities.Orders
{
    public class CustomerAddress : Address
    {
        public CustomerAddress() {}

        public CustomerAddress(string zipCode, string city, string state, string country, string line1) : base(zipCode, city, state, country, line1)
        {
        }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("created_at")]
        public string CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public string UpdatedAt { get; set; }

        public void SetStatus(string status)
        {
            if (!ValidateOrder.IsValidAddressStatus(status))
                throw new ApplicationException("Status must be active or deleted!");

            Status = status;
        }

        public void SetCreatedAt(string date)
        {
            if (!ValidateOrder.IsValidDate(date))
                throw new ApplicationException("Date is not in correct format!");

            CreatedAt = date;
        }

        public void SetDocument(string date)
        {
            if (!ValidateOrder.IsValidDate(date))
                throw new ApplicationException("Date is not in correct format!");

            UpdatedAt = date;
        }
    }
}
