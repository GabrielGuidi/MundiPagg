using MundiPagg.Domain.Shared;
using System;
using System.Text.Json.Serialization;

namespace MundiPagg.Domain.Orders.Entities.Orders
{
    public class Customer
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; private set; }

        [JsonPropertyName("document")]
        public string Document { get; private set; }

        [JsonPropertyName("type")]
        public string Type { get; private set; }

        [JsonPropertyName("delinquent")]
        public bool? Delinquent { get; set; }

        [JsonPropertyName("address")]
        public CustomerAddress Address { get; set; }

        [JsonPropertyName("created_at")]
        public string CreatedAt { get; private set; }

        [JsonPropertyName("updated_at")]
        public string UpdatedAt { get; private set; }

        [JsonPropertyName("birthdate")]
        public string Birthdate { get; private set; }

        [JsonPropertyName("phones")]
        public Contacts Phones { get; set; }

        public void SetDocument(string document)
        {
            if (!ValidateOrder.IsValidDocument(document))
                throw new ApplicationException("Document must have 11 or 14 characters!");
            
            Document = document;
        }

        public void SetEmail(string email)
        {
            if (!ValidateOrder.IsValidEmail(email))
                throw new ApplicationException("Document must have 11 or 14 characters!");

            Email = email;
        }

        public void SetType(string type)
        {
            if (!ValidateOrder.IsValidCustomerType(type))
                throw new ApplicationException("Accepted values for Type are: individual and company!");

            Type = type;
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

        public void SetBirthdate(string date)
        {
            if (!ValidateOrder.IsValidDate(date))
                throw new ApplicationException("Date is not in correct format!");

            Birthdate = date;
        }
    }
}
