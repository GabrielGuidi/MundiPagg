using System.Text.Json.Serialization;

namespace MundiPagg.AppService.Models
{
    public class Entrega
    {
        [JsonPropertyName("endereco_entrega")]
        public Endereco EnderecoEntrega { get; set; }

        [JsonPropertyName("frete")]
        public decimal Frete { get; set; }

        [JsonPropertyName("shipping_company")]
        public string ShippingCompany { get; set; }
    }
}
