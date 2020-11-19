using System.Text.Json.Serialization;

namespace MundiPagg.Domain.CreateOrders.Entities
{
    public class Payment
    {
        [JsonPropertyName("payment_method")]
        public string PaymentMethod { get; set; }

        [JsonPropertyName("credit_card")]
        public CreditCard CreditCard { get; set; }
    }
}
