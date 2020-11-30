using MundiPagg.Domain.Orders.Entities.Orders;
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
                HomePhone = (Phone)newOrderPhone.HomePhone,
                MobilePhone = (Phone)newOrderPhone.MobilePhone
            };
        }
    }

    public class PhoneDescription
    {
        [JsonPropertyName("country_code")]
        public string CountryCode { get; set; }

        [JsonPropertyName("area_code")]
        public string AreaCode { get; set; }

        [JsonPropertyName("number")]
        public string Number { get; set; }

        public static explicit operator PhoneDescription(string telefone)
        {
            return new PhoneDescription()
            {
                CountryCode = telefone.Substring(0, 2),
                AreaCode = telefone.Substring(2, 2),
                Number = telefone.Substring(4),
            };
        }

        public static explicit operator Phone(PhoneDescription phoneDescription)
        {
            var phone = new Phone();

            phone.SetAreaCode(phoneDescription.CountryCode);
            phone.SetAreaCode(phoneDescription.AreaCode);
            phone.SetNumber(phoneDescription.Number);

            return phone;
        }
    }
}
