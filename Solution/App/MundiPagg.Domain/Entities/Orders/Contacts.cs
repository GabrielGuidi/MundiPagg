using System.Text.Json.Serialization;

namespace MundiPagg.Domain.Orders.Entities.Orders
{
    public class Contacts
    {
        [JsonPropertyName("home_phone")]
        public Phone HomePhone { get; set; }

        [JsonPropertyName("mobile_phone")]
        public Phone MobilePhone { get; set; }
    }
}
