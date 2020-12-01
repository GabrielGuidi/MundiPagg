﻿using MundiPagg.Domain.Shared;
using System;
using System.Text.Json.Serialization;

namespace MundiPagg.Domain.Orders.Entities.Orders
{
    public class CustomerAddress : Address
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; private set; }

        [JsonPropertyName("created_at")]
        public string CreatedAt { get; private set; }

        [JsonPropertyName("updated_at")]
        public string UpdatedAt { get; private set; }

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