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
            var amount = FormatOrderData.ConvertToLong(entrega.Frete);
            var description = entrega.ShippingCompany;
            var address = (Address)entrega.EnderecoEntrega;

            return new NewOrderShipping(amount, description, address);
        }
    }
}
