using System.Text.Json.Serialization;
namespace MundiPagg.AppService.Models
{
    public class Endereco
    {
        [JsonPropertyName("cidade")]
        public string Cidade { get; set; }

        [JsonPropertyName("complemento")]
        public string Complemento { get; set; }

        [JsonPropertyName("pais")]
        public string Pais { get; set; }

        [JsonPropertyName("bairro")]
        public string Bairro { get; set; }

        [JsonPropertyName("numero")]
        public string Numero { get; set; }

        [JsonPropertyName("estado")]
        public string Estado { get; set; }

        [JsonPropertyName("logradouro")]
        public string Logradouro { get; set; }

        [JsonPropertyName("cep")]
        public string Cep { get; set; }
    }
}
