using System.Text.Json.Serialization;


namespace MundiPagg.Domain.CreateOrders.Entities.NewOrders
{
    public class NewOrderCreditCard
    {
        public NewOrderCreditCard()
        {
            OperationType = "auth_and_capture";
            Capture = true;
        }

        [JsonPropertyName("installments")]
        public long Installments { get; set; }

        [JsonPropertyName("operation_type")]
        public string OperationType { get; set; }

        [JsonPropertyName("capture")]
        public bool Capture { get; set; }

        [JsonPropertyName("card")]
        public NewOrderCard Card { get; set; }
    }
}
