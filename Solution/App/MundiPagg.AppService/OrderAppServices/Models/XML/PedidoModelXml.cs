using MundiPagg.AppService.Shared;
using MundiPagg.Domain.CreateOrders.Entities.NewOrders;
using MundiPagg.Domain.Orders.Entities.NewOrders;
using MundiPagg.Domain.Orders.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace MundiPagg.AppService.OrderAppServices.Models.XML
{
    [XmlRoot(ElementName = "root")]
    public class PedidoModelXml
    {
        [XmlElement(ElementName = "numero_pedido")]
        public string Numero_pedido { get; set; }
        [XmlElement(ElementName = "comprador")]
        public Comprador Comprador { get; set; }
        [XmlElement(ElementName = "pagamento")]
        public Pagamento Pagamento { get; set; }
        [XmlElement(ElementName = "entrega")]
        public Entrega Entrega { get; set; }
        [XmlElement(ElementName = "carrinho")]
        public Carrinho Carrinho { get; set; }

        public static explicit operator NewOrder(PedidoModelXml model)
        {
            var customer = (NewOrderCustomer)model.Comprador;
            var payments = new NewOrderPayment[] { (NewOrderPayment)model.Pagamento };
            var items = model.Carrinho.Items.Select(x => (NewOrderItem)x).ToArray();

            var order = new NewOrder(customer, items, payments)
            {
                Code = model.Numero_pedido,
                Shipping = (NewOrderShipping)model.Entrega
            };

            return order;
        }
    }

    [XmlRoot(ElementName = "endereco")]
    public class Endereco
    {
        [XmlElement(ElementName = "cidade")]
        public string Cidade { get; set; }
        [XmlElement(ElementName = "complemento")]
        public string Complemento { get; set; }
        [XmlElement(ElementName = "pais")]
        public string Pais { get; set; }
        [XmlElement(ElementName = "bairro")]
        public string Bairro { get; set; }
        [XmlElement(ElementName = "numero")]
        public string Numero { get; set; }
        [XmlElement(ElementName = "estado")]
        public string Estado { get; set; }
        [XmlElement(ElementName = "logradouro")]
        public string Logradouro { get; set; }
        [XmlElement(ElementName = "cep")]
        public string Cep { get; set; }

        public static explicit operator Address(Endereco endereco)
        {
            var address = new Address(
                endereco.Cep, endereco.Cidade, endereco.Estado.ToUpper(),
                ConvertToCountry(endereco.Pais), $"{endereco.Numero}, {endereco.Logradouro}, {endereco.Bairro}")
            {
                Line2 = endereco.Complemento
            };

            return address;
        }

        private static string ConvertToCountry(string pais)
        {
            if (pais.ToUpper() == "BRAZIL")
            {
                return "BR";
            }

            throw new ApplicationException("Can't convert Country!");
        }
    }

    [XmlRoot(ElementName = "comprador")]
    public class Comprador
    {
        [XmlElement(ElementName = "aniversario")]
        public string Aniversario { get; set; }
        [XmlElement(ElementName = "documento")]
        public string Documento { get; set; }
        [XmlElement(ElementName = "email")]
        public string Email { get; set; }
        [XmlElement(ElementName = "telefone")]
        public string Telefone { get; set; }
        [XmlElement(ElementName = "celular")]
        public string Celular { get; set; }
        [XmlElement(ElementName = "nome")]
        public string Nome { get; set; }
        [XmlElement(ElementName = "tipo")]
        public string Tipo { get; set; }
        [XmlElement(ElementName = "endereco")]
        public Endereco Endereco { get; set; }

        public static explicit operator NewOrderCustomer(Comprador comprador)
        {
            var type = ConvertToTyoe(comprador.Tipo);
            var birthdate = comprador.Aniversario;
            var document = comprador.Documento;
            var email = comprador.Email;

            return new NewOrderCustomer(type, birthdate, document, email)
            {
                Name = comprador.Nome,
                Phones = ConvertToPhones(comprador.Celular, comprador.Telefone),
                Address = (Address)comprador.Endereco
            };
        }

        private static string ConvertToTyoe(string tipo)
        {
            if (tipo == "pessoa_fisica")
            {
                return "individual";
            };

            throw new ApplicationException("Can't convert Type!");
        }

        private static NewOrderPhone ConvertToPhones(string celular, string telefone)
        {
            return new NewOrderPhone()
            {
                MobilePhone = (PhoneDescription)celular,
                HomePhone = (PhoneDescription)telefone
            };
        }
    }

    [XmlRoot(ElementName = "endereco_cobranca")]
    public class Endereco_cobranca
    {
        [XmlElement(ElementName = "cidade")]
        public string Cidade { get; set; }
        [XmlElement(ElementName = "complemento")]
        public string Complemento { get; set; }
        [XmlElement(ElementName = "pais")]
        public string Pais { get; set; }
        [XmlElement(ElementName = "bairro")]
        public string Bairro { get; set; }
        [XmlElement(ElementName = "numero")]
        public string Numero { get; set; }
        [XmlElement(ElementName = "estado")]
        public string Estado { get; set; }
        [XmlElement(ElementName = "logradouro")]
        public string Logradouro { get; set; }
        [XmlElement(ElementName = "cep")]
        public string Cep { get; set; }

        public static explicit operator Address(Endereco_cobranca endereco)
        {
            var address = new Address(
                endereco.Cep, endereco.Cidade, endereco.Estado.ToUpper(),
                ConvertToCountry(endereco.Pais), $"{endereco.Numero}, {endereco.Logradouro}, {endereco.Bairro}")
            {
                Line2 = endereco.Complemento
            };

            return address;
        }

        private static string ConvertToCountry(string pais)
        {
            if (pais.ToUpper() == "BRAZIL")
            {
                return "BR";
            }

            throw new ApplicationException("Can't convert Country!");
        }
    }

    [XmlRoot(ElementName = "cartao")]
    public class Cartao
    {
        [XmlElement(ElementName = "bandeira")]
        public string Bandeira { get; set; }
        [XmlElement(ElementName = "numero_cartao")]
        public string Numero_cartao { get; set; }
        [XmlElement(ElementName = "mes_vencimento")]
        public string Mes_vencimento { get; set; }
        [XmlElement(ElementName = "ano_vencimento")]
        public string Ano_vencimento { get; set; }
        [XmlElement(ElementName = "nome_cartao")]
        public string Nome_cartao { get; set; }
        [XmlElement(ElementName = "cvv")]
        public string Cvv { get; set; }
    }

    [XmlRoot(ElementName = "pagamento")]
    public class Pagamento
    {
        [XmlElement(ElementName = "valor")]
        public string Valor { get; set; }
        [XmlElement(ElementName = "parcelas")]
        public string Parcelas { get; set; }
        [XmlElement(ElementName = "endereco_cobranca")]
        public Endereco_cobranca Endereco_cobranca { get; set; }
        [XmlElement(ElementName = "cartao")]
        public Cartao Cartao { get; set; }

        public static explicit operator NewOrderPayment(Pagamento pagamento)
        {
            var amount = FormatOrderData.ConvertToLong(decimal.Parse(pagamento.Valor));
            return new NewOrderPayment(amount)
            {
                PaymentMethod = ConvertToPaymentMethod(pagamento),
                CreditCard = (NewOrderCreditCard)pagamento
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
                Number = pagamento.Cartao.Numero_cartao,
                HolderName = pagamento.Cartao.Nome_cartao,
                ExpMonth = long.Parse(pagamento.Cartao.Mes_vencimento),
                ExpYear = long.Parse(pagamento.Cartao.Ano_vencimento),
                CVV = pagamento.Cartao.Cvv,
                BillingAddress = (Address)pagamento.Endereco_cobranca
            };
        }
    }

    [XmlRoot(ElementName = "endereco_entrega")]
    public class Endereco_entrega
    {
        [XmlElement(ElementName = "cidade")]
        public string Cidade { get; set; }
        [XmlElement(ElementName = "complemento")]
        public string Complemento { get; set; }
        [XmlElement(ElementName = "pais")]
        public string Pais { get; set; }
        [XmlElement(ElementName = "bairro")]
        public string Bairro { get; set; }
        [XmlElement(ElementName = "numero")]
        public string Numero { get; set; }
        [XmlElement(ElementName = "estado")]
        public string Estado { get; set; }
        [XmlElement(ElementName = "logradouro")]
        public string Logradouro { get; set; }
        [XmlElement(ElementName = "cep")]
        public string Cep { get; set; }

        public static explicit operator Address(Endereco_entrega endereco)
        {
            var address = new Address(
                endereco.Cep, endereco.Cidade, endereco.Estado.ToUpper(),
                ConvertToCountry(endereco.Pais), $"{endereco.Numero}, {endereco.Logradouro}, {endereco.Bairro}")
            {
                Line2 = endereco.Complemento
            };

            return address;
        }

        private static string ConvertToCountry(string pais)
        {
            if (pais.ToUpper() == "BRAZIL")
            {
                return "BR";
            }

            throw new ApplicationException("Can't convert Country!");
        }
    }

    [XmlRoot(ElementName = "entrega")]
    public class Entrega
    {
        [XmlElement(ElementName = "endereco_entrega")]
        public Endereco_entrega Endereco_entrega { get; set; }
        [XmlElement(ElementName = "frete")]
        public string Frete { get; set; }
        [XmlElement(ElementName = "shipping_company")]
        public string Shipping_company { get; set; }

        public static explicit operator NewOrderShipping(Entrega entrega)
        {
            var amount = FormatOrderData.ConvertToLong(decimal.Parse(entrega.Frete));
            var description = entrega.Shipping_company;
            var address = (Address)entrega.Endereco_entrega;

            return new NewOrderShipping(amount, description, address);
        }
    }

    [XmlRoot(ElementName = "items")]
    public class Items
    {
        [XmlElement(ElementName = "produto")]
        public string Produto { get; set; }
        [XmlElement(ElementName = "quantidade")]
        public string Quantidade { get; set; }
        [XmlElement(ElementName = "valor_unit")]
        public string Valor_unit { get; set; }

        public static explicit operator NewOrderItem(Items item)
        {
            var amount = FormatOrderData.ConvertToLong(decimal.Parse(item.Valor_unit));
            var quantity = FormatOrderData.ConvertToLong(decimal.Parse(item.Quantidade));
            var description = item.Produto;

            return new NewOrderItem(description, quantity, amount);
        }
    }

    [XmlRoot(ElementName = "carrinho")]
    public class Carrinho
    {
        [XmlElement(ElementName = "items")]
        public List<Items> Items { get; set; }
    }
}
