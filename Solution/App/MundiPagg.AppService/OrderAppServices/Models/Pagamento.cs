using MundiPagg.AppService.Shared;
using MundiPagg.Domain.CreateOrders.Entities.NewOrders;
using MundiPagg.Domain.Orders.Entities.Orders;
using System;
using System.Text.Json.Serialization;

namespace MundiPagg.AppService.Models
{
    public class Pagamento
    {
        [JsonPropertyName("valor")]
        public decimal Valor { get; set; }

        [JsonPropertyName("parcelas")]
        public string Parcelas { get; set; }

        [JsonPropertyName("endereco_cobranca")]
        public Endereco EnderecoCobranca { get; set; }

        [JsonPropertyName("cartao")]
        public Cartao Cartao { get; set; }

        public static explicit operator NewOrderPayment(Pagamento pagamento)
        {
            var amount = FormatOrderData.ConvertToLong(pagamento.Valor);
            return new NewOrderPayment(amount)
            {
                PaymentMethod = ConvertToPaymentMethod(pagamento),
                CreditCard = (NewOrderCreditCard)pagamento
            };
        }

        public static explicit operator NewOrderCreditCard(Pagamento pagamento)
        {
            return new NewOrderCreditCard()
            {
                Installments = long.Parse(pagamento.Parcelas),
                Card = (NewOrderCard)pagamento
            };
        }

        public static explicit operator NewOrderCard(Pagamento pagamento)
        {
            return new NewOrderCard()
            {
                Brand = pagamento.Cartao.Bandeira,
                Number = pagamento.Cartao.NumeroCartao,
                HolderName = pagamento.Cartao.NomeCartao,
                ExpMonth = long.Parse(pagamento.Cartao.MesVencimento),
                ExpYear = long.Parse(pagamento.Cartao.AnoVencimento),
                CVV = pagamento.Cartao.Cvv,
                BillingAddress = (Address)pagamento.EnderecoCobranca
            };
        }

        private static string ConvertToPaymentMethod(Pagamento pagamento)
        {
            if (pagamento.Cartao != null)
            {
                return "credit_card";
            }

            throw new ApplicationException("Can't convert PaymentMethod!");
        }
    }
}
