using System.Text.Json.Serialization;


namespace MundiPagg.Domain.CreateOrders.Entities
{
    public class CreditCard
    {
        [JsonPropertyName("recurrence")]
        public bool Recurrence { get; set; }

        [JsonPropertyName("installments")]
        public long Installments { get; set; }

        [JsonPropertyName("statement_descriptor")]
        public string StatementDescriptor { get; set; }

        [JsonPropertyName("card")]
        public Card Card { get; set; }
    }
}
