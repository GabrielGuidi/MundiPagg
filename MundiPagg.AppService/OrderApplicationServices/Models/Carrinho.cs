using System.Text.Json.Serialization;

namespace MundiPagg.AppService.Models
{
    public class Carrinho
    {
        [JsonPropertyName("items")]
        public Item[] Items { get; set; }
    }
}
