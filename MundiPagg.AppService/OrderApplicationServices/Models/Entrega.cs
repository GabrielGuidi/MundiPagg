using MundiPagg.AppService.Shared;
using MundiPagg.Domain.CreateOrders.Entities.NewOrders;
using MundiPagg.Domain.Orders.Entities.Orders;
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

        public static explicit operator NewOrderShipping(Entrega entrega)
        {
            return new NewOrderShipping()
            {
                Amount = FormatOrderData.ConvertToLong(entrega.Frete),
                Description = entrega.ShippingCompany,
                Address = (Address)entrega.EnderecoEntrega
            };
        }
    }
}
