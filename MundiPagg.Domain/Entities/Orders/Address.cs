using MundiPagg.Domain.Shared;
using System;
using System.Text.Json.Serialization;

namespace MundiPagg.Domain.Orders.Entities.Orders
{
    public class Address
    {
        public Address(string zipCode, string city, string state, string country, string line1)
        {
            SetZipCode(zipCode);
            SetLine1(line1);
            City = city;
            State = state;
            Country = country;
            Line2 = string.Empty;
        }

        [JsonPropertyName("zip_code")]
        public string ZipCode { get; private set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("state")]
        public string State { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("line_1")]
        public string Line1 { get; private set; }

        [JsonPropertyName("line_2")]
        public string Line2 { get; set; }

        public void SetZipCode(string zipCode)
        {
            if (!ValidateOrder.IsNumeric(zipCode))
                throw new ApplicationException("Zip code must be numeric!");

            ZipCode = zipCode;
        }

        public void SetLine1(string line1)
        {
            if (!ValidateOrder.IsValidLine1(line1))
                throw new ApplicationException("Line1 must be in the format 'número, rua, bairro'!");

            Line1 = line1;
        }
    }
}
