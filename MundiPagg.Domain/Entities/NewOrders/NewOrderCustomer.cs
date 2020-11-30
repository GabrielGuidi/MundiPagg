using MundiPagg.Domain.Orders.Entities.NewOrders;
using MundiPagg.Domain.Orders.Entities.Orders;
using MundiPagg.Domain.Shared;
using System;
using System.Text.Json.Serialization;


namespace MundiPagg.Domain.CreateOrders.Entities.NewOrders
{
    public class NewOrderCustomer
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("birthdate")]
        public string Birthdate { get; set; }

        [JsonPropertyName("document")]
        public string Document { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("phones")]
        public NewOrderPhone Phones { get; set; }

        [JsonPropertyName("address")]
        public Address Address { get; set; }

        public static explicit operator Customer(NewOrderCustomer newOrderCustomer)
        {
            var customer = new Customer
            {
                Name = newOrderCustomer.Name,
                Delinquent = null,
                Address = CreateAddress(newOrderCustomer.Address),
                Phones = (Contacts)newOrderCustomer.Phones
            };

            customer.SetEmail(newOrderCustomer.Email);
            customer.SetDocument(newOrderCustomer.Document);
            customer.SetType(newOrderCustomer.Type);
            customer.SetCreatedAt(FormatOrder.ToStringDate(DateTime.Now));
            customer.SetBirthdate(newOrderCustomer.Birthdate);

            return customer;
        }

        private static CustomerAddress CreateAddress(Address address)
        {
            var customerAddress = new CustomerAddress
            {
                Line2 = address.Line2,
                City = address.City,
                State = address.State,
                Country = address.Country
            };

            customerAddress.SetLine1(address.Line1);
            customerAddress.SetZipCode(address.ZipCode);
            customerAddress.SetCreatedAt(FormatOrder.ToStringDate(DateTime.Now));

            return customerAddress;
        }
    }
}
