using MundiPagg.Domain.Orders.Entities.Orders;
using MundiPagg.Domain.Shared;
using System;
using System.Text.Json.Serialization;

namespace MundiPagg.Domain.Orders.Entities.NewOrders
{
    public class NewOrderPhone
    {
        [JsonPropertyName("home_phone")]
        public PhoneDescription HomePhone { get; set; }

        [JsonPropertyName("mobile_phone")]
        public PhoneDescription MobilePhone { get; set; }

        public static explicit operator Contacts(NewOrderPhone newOrderPhone)
        {
            return new Contacts
            {
                HomePhone = (Phone)newOrderPhone?.HomePhone,
                MobilePhone = (Phone)newOrderPhone?.MobilePhone
            };
        }
    }

    public class PhoneDescription
    {
        [JsonPropertyName("country_code")]
        public string CountryCode { get; private set; }

        [JsonPropertyName("area_code")]
        public string AreaCode { get; private set; }

        [JsonPropertyName("number")]
        public string Number { get; private set; }

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


        public static explicit operator PhoneDescription(string telefone)
        {
            return new PhoneDescription()
            {
                CountryCode = telefone.Substring(0, 2),
                AreaCode = telefone.Substring(2, 2),
                Number = telefone[4..],
            };
        }

        public static explicit operator Phone(PhoneDescription phoneDescription)
        {
            if (phoneDescription is null)
                return null;

            var phone = new Phone();

            phone.SetAreaCode(phoneDescription.CountryCode);
            phone.SetAreaCode(phoneDescription.AreaCode);
            phone.SetNumber(phoneDescription.Number);

            return phone;
        }
    }
}
