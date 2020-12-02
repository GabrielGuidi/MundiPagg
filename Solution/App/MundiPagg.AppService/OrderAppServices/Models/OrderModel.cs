using MundiPagg.Domain.CreateOrders.Entities.NewOrders;
using System.Linq;
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
        public static explicit operator NewOrder(OrderModel orderModel)
        {
            var customer = (NewOrderCustomer)orderModel.Comprador;
            var payments = new NewOrderPayment[] { (NewOrderPayment)orderModel.Pagamento };
            var items = orderModel.Carrinho.Items.Select(x => (NewOrderItem)x).ToArray();

            var order = new NewOrder(customer, items, payments)
            {
                Code = orderModel.NumeroPedido,
                Shipping = (NewOrderShipping)orderModel.Entrega
            };

            return order;
        }
        #endregion
    }
}
