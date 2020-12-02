using MundiPagg.Domain.Orders.Entities.NewOrders;
using MundiPagg.Domain.Orders.Entities.Orders;
using MundiPagg.Domain.Shared;
using System;
using System.Text.Json.Serialization;


namespace MundiPagg.Domain.CreateOrders.Entities.NewOrders
{
    public class NewOrderCustomer
    {
        public NewOrderCustomer(string type, string birthdate, string document, string email)
        {
            Type = type;
            Birthdate = birthdate;
            Document = document;
            Email = email;
        }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; private set; }

        [JsonPropertyName("birthdate")]
        public string Birthdate { get; private set; }

        [JsonPropertyName("document")]
        public string Document { get; private set; }

        [JsonPropertyName("email")]
        public string Email { get; private set; }

        [JsonPropertyName("phones")]
        public NewOrderPhone Phones { get; set; }

        [JsonPropertyName("address")]
        public Address Address { get; set; }

        public void SetDocument(string document)
        {
            if (!ValidateOrder.IsValidDocument(document))
                throw new ApplicationException("Document must have 11 or 14 characters!");

            Document = document;
        }

        public void SetEmail(string email)
        {
            if (!ValidateOrder.IsValidEmail(email))
                throw new ApplicationException("Document must have 11 or 14 characters!");

            Email = email;
        }

        public void SetType(string type)
        {
            if (!ValidateOrder.IsValidCustomerType(type))
                throw new ApplicationException("Accepted values for Type are: individual and company!");

            Type = type;
        }

        public void SetBirthdate(string date)
        {
            if (!ValidateOrder.IsValidDate(date))
                throw new ApplicationException("Date is not in correct format!");

            Birthdate = date;
        }


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
            var city = address.City;
            var state = address.State;
            var country = address.Country;

            var customerAddress = new CustomerAddress(address.ZipCode, city, state, country , address.Line1)
            {
                Line2 = address.Line2,
                
            };

            customerAddress.SetLine1(address.Line1);
            customerAddress.SetZipCode(address.ZipCode);
            customerAddress.SetCreatedAt(FormatOrder.ToStringDate(DateTime.Now));

            return customerAddress;
        }
    }
}
