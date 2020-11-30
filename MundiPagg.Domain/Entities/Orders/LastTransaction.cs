using System.Text.Json.Serialization;

namespace MundiPagg.Domain.Orders.Entities.Orders
{
    public class LastTransaction
    {
        [JsonPropertyName("operation_key")]
        public string OperationKey { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("transaction_type")]
        public string TransactionType { get; set; }

        [JsonPropertyName("gateway_id")]
        public string GatewayId { get; set; }

        [JsonPropertyName("amount")]
        public long Amount { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("installments")]
        public long Installments { get; set; }

        [JsonPropertyName("acquirer_name")]
        public string AcquirerName { get; set; }

        [JsonPropertyName("acquirer_tid")]
        public string AcquirerTid { get; set; }

        [JsonPropertyName("acquirer_nsu")]
        public string AcquirerNsu { get; set; }

        [JsonPropertyName("acquirer_auth_code")]
        public string AcquirerAuthCode { get; set; }

        [JsonPropertyName("acquirer_message")]
        public string AcquirerMessage { get; set; }

        [JsonPropertyName("acquirer_return_code")]
        public string AcquirerReturnCode { get; set; }

        [JsonPropertyName("operation_type")]
        public string OperationType { get; set; }

        [JsonPropertyName("card")]
        public Card Card { get; set; }

        [JsonPropertyName("created_at")]
        public string CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public string UpdatedAt { get; set; }

        [JsonPropertyName("gateway_response")]
        public Gateway GatewayResponse { get; set; }

        [JsonPropertyName("antifraud_response")]
        public Antifraud AntifraudResponse { get; set; }

        [JsonPropertyName("metadata")]
        public Antifraud Metadata { get; set; }
    }
}
