using System.Text.Json.Serialization;

namespace MundiPagg.Domain.CreateOrders.Entities.NewOrders
{
    public class NewOrderPayment
    {
        public NewOrderPayment(long amount)
        {
            Amount = amount;
        }

        [JsonPropertyName("amount")]
        public long Amount { get; set; }

        [JsonPropertyName("payment_method")]
        public string PaymentMethod { get; set; }

        [JsonPropertyName("credit_card")]
        public NewOrderCreditCard CreditCard { get; set; }
    }
}
