using System.Text.Json.Serialization;

namespace MundiPagg.AppService.Models
{
    public class Cartao
    {
        [JsonPropertyName("bandeira")]
        public string Bandeira { get; set; }

        [JsonPropertyName("numero_cartao")]
        public string NumeroCartao { get; set; }

        [JsonPropertyName("mes_vencimento")]
        public string MesVencimento { get; set; }

        [JsonPropertyName("ano_vencimento")]
        public string AnoVencimento { get; set; }

        [JsonPropertyName("nome_cartao")]
        public string NomeCartao { get; set; }

        [JsonPropertyName("cvv")]
        public string Cvv { get; set; }
    }
}
