using MundiPagg.Domain.CreateOrders.Entities;
using System;
using System.Text.Json.Serialization;

namespace MundiPagg.AppService.Models
{
    public class OrderModel
    {
        [JsonPropertyName("numero_pedido")]
        public string NumeroPedido { get; set; }

        [JsonPropertyName("comprador")]
        public Comprador Comprador { get; set; }

        [JsonPropertyName("pagamento")]
        public Pagamento Pagamento { get; set; }

        [JsonPropertyName("entrega")]
        public Entrega Entrega { get; set; }

        [JsonPropertyName("carrinho")]
        public Carrinho Carrinho { get; set; }

        #region [Mapping]
        public static explicit operator Order(OrderModel orderModel)
        {
            return new Order();
        }
        #endregion
    }
}
