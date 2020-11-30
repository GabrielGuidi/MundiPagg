using MundiPagg.AppService.Shared;
using MundiPagg.Domain.CreateOrders.Entities.NewOrders;
using System.Text.Json.Serialization;

namespace MundiPagg.AppService.Models
{
    public class ModelItem
    {
        [JsonPropertyName("produto")]
        public string Produto { get; set; }

        [JsonPropertyName("quantidade")]
        public decimal Quantidade { get; set; }

        [JsonPropertyName("valor_unit")]
        public decimal ValorUnit { get; set; }

        public static explicit operator NewOrderItem(ModelItem item)
        {
            return new NewOrderItem()
            {
                Amount = FormatOrderData.ConvertToLong(item.ValorUnit),
                Quantity = FormatOrderData.ConvertToLong(item.Quantidade),
                Description = item.Produto
            };
        }
    }
}
