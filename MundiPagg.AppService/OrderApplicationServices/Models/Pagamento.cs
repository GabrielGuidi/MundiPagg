using System.Text.Json.Serialization;

namespace MundiPagg.AppService.Models
{
    public class Pagamento
    {
        [JsonPropertyName("valor")]
        public decimal Valor { get; set; }

        [JsonPropertyName("parcelas")]
        public string Parcelas { get; set; }

        [JsonPropertyName("endereco_cobranca")]
        public Endereco EnderecoCobranca { get; set; }

        [JsonPropertyName("cartao")]
        public Cartao Cartao { get; set; }
    }
}
