using System;
using System.Text.Json.Serialization;

namespace MundiPagg.AppService.Models
{
    public class Comprador
    {
        [JsonPropertyName("aniversario")]
        public DateTimeOffset Aniversario { get; set; }

        [JsonPropertyName("documento")]
        public string Documento { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("telefone")]
        public string Telefone { get; set; }

        [JsonPropertyName("celular")]
        public string Celular { get; set; }

        [JsonPropertyName("nome")]
        public string Nome { get; set; }

        [JsonPropertyName("tipo")]
        public string Tipo { get; set; }

        [JsonPropertyName("endereco")]
        public Endereco Endereco { get; set; }
    }
}
