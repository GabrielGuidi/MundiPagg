using MundiPagg.Domain.CreateOrders.Entities.NewOrders;
using MundiPagg.Domain.Orders.Entities.NewOrders;
using MundiPagg.Domain.Orders.Entities.Orders;
using System;
using System.Text.Json.Serialization;

namespace MundiPagg.AppService.Models
{
    public class Comprador
    {
        [JsonPropertyName("aniversario")]
        public string Aniversario { get; set; }

        [JsonPropertyName("documento")]
        public string Documento { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("telefone")]
        public string Telefone { get; set; }

        [JsonPropertyName("celular")]
        public string Celular { get; set; }

        [JsonPropertyName("nome")]
        public string Nome { get; set; }

        [JsonPropertyName("tipo")]
        public string Tipo { get; set; }

        [JsonPropertyName("endereco")]
        public Endereco Endereco { get; set; }

        public static explicit operator NewOrderCustomer(Comprador comprador)
        {
            return new NewOrderCustomer()
            {
                Email = comprador.Email,
                Name = comprador.Nome,
                Type = ConvertToTyoe(comprador.Tipo),
                Birthdate = comprador.Aniversario,
                Document = comprador.Documento,
                Phones = ConvertToPhones(comprador.Celular, comprador.Telefone),
                Address = (Address)comprador.Endereco
            };
        }

        private static string ConvertToTyoe(string tipo)
        {
            if (tipo== "pessoa_fisica")
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
}
