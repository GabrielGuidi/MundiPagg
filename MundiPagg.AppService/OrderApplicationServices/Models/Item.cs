using System.Text.Json.Serialization;

namespace MundiPagg.AppService.Models
{
    public class Item
    {
        [JsonPropertyName("produto")]
        public string Produto { get; set; }

        [JsonPropertyName("quantidade")]
        public decimal Quantidade { get; set; }

        [JsonPropertyName("valor_unit")]
        public decimal ValorUnit { get; set; }
    }
}
