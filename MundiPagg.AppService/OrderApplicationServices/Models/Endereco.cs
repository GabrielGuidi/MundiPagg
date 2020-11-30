using MundiPagg.Domain.Orders.Entities.Orders;
using System;
using System.Text.Json.Serialization;
namespace MundiPagg.AppService.Models
{
    public class Endereco
    {
        [JsonPropertyName("cidade")]
        public string Cidade { get; set; }

        [JsonPropertyName("complemento")]
        public string Complemento { get; set; }

        [JsonPropertyName("pais")]
        public string Pais { get; set; }

        [JsonPropertyName("bairro")]
        public string Bairro { get; set; }

        [JsonPropertyName("numero")]
        public string Numero { get; set; }

        [JsonPropertyName("estado")]
        public string Estado { get; set; }

        [JsonPropertyName("logradouro")]
        public string Logradouro { get; set; }

        [JsonPropertyName("cep")]
        public string Cep { get; set; }

        public static explicit operator Address(Endereco endereco)
        {
            var address = new Address()
            {
                City = endereco.Cidade,
                Country = ConvertToCountry(endereco.Pais),
                State = endereco.Estado.ToUpper(),
                Line2 = endereco.Complemento
            };

            address.SetZipCode(endereco.Cep);
            address.SetLine1($"{endereco.Numero}, {endereco.Logradouro}, {endereco.Bairro}");

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
}
