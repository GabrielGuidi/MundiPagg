using MundiPagg.Domain.Orders.Entities.Orders;
using MundiPagg.Domain.Shared;
using System;
using System.Linq;
using System.Text.Json.Serialization;

namespace MundiPagg.Domain.CreateOrders.Entities.NewOrders
{
    public class NewOrder
    {
        public NewOrder(NewOrderCustomer customer, NewOrderItem[] items, NewOrderPayment[] payments)
        {
            Customer = customer;
            Items = items;
            Payments = payments;
        }

        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("customer")]
        public NewOrderCustomer Customer { get; set; }

        [JsonPropertyName("payments")]
        public NewOrderPayment[] Payments { get; set; }

        [JsonPropertyName("shipping")]
        public NewOrderShipping Shipping { get; set; }

        [JsonPropertyName("items")]
        public NewOrderItem[] Items { get; set; }

        public static explicit operator Order(NewOrder newOrder)
        {
            var items = newOrder.Items.Select(x => (Item)x).ToArray();
            var customer = (Customer)newOrder.Customer;

            return new Order(items, customer)
            {
                Code = newOrder.Code,
                Amount = newOrder.Payments.Sum(x => x.Amount),
                Shipping = (Shipping)newOrder.Shipping,
                Status = "Created",
                CreatedAt = FormatOrder.ToStringDate(DateTime.Now),
                UpdatedAt = string.Empty,
                Charges = CreateCharges(newOrder)
            };
        }

        private static Charge[] CreateCharges(NewOrder newOrder)
        {
            var charge = new Charge
            {
                Code = newOrder.Code,
                Amount = newOrder.Payments.Sum(x => x.Amount),
                PaidAmount = 0,
                Currency = "BRL",
                PaymentMethod = newOrder.Payments.FirstOrDefault()?.PaymentMethod ?? "not-found",
                Customer = (Customer)newOrder.Customer,
                LastTransaction = null
            };

            charge.SetCreatedAt(DateTime.Now.ToString());
            charge.SetStatus("pending");

            return new Charge[] { charge };
        }
    }
}
