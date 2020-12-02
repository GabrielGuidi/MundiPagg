using MundiPagg.Domain.Shared;
using System;
using System.Text.Json.Serialization;

namespace MundiPagg.Domain.Orders.Entities.Orders
{
    public class Phone
    {
        [JsonPropertyName("country_code")]
        public string CountryCode { get; set; }

        [JsonPropertyName("number")]
        public string Number { get; set; }

        [JsonPropertyName("area_code")]
        public string AreaCode { get; set; }

        public void SetCountryCode(string countryCode)
        {
            if (!ValidateOrder.IsNumeric(countryCode))
                throw new ApplicationException("CountryCode must be numeric!");

            CountryCode = countryCode;
        }

        public void SetNumber(string number)
        {
            if (!ValidateOrder.IsNumeric(number))
                throw new ApplicationException("Number must be numeric!");

            Number = number;
        }

        public void SetAreaCode(string areaCode)
        {
            if (!ValidateOrder.IsNumeric(areaCode))
                throw new ApplicationException("AreaCode must be numeric!");

            AreaCode = areaCode;
        }
    }
}
