using MundiPagg.Domain.CreateOrders.Entities.NewOrders;
using MundiPagg.Domain.Orders.Entities.Orders;
using Xunit;

namespace MundiPagg.Tests.Domain.OrderTest
{
    public class NewOrderTest
    {
        #region [Objetcs]
        private readonly Address address;
        private readonly NewOrderCustomer customer;
        private readonly NewOrder newOrder;

        public NewOrderTest()
        {
            var birthdate = "1991-02-05";
            var type = "individual";
            var document = "11111111111";
            var email = "email@email.com";

            address = new Address("29101200", "ABC", "ES", "BR", "012, DEF, GHF");
            customer = new NewOrderCustomer(type, birthdate, document, email)
            {
                Address = address
            };

            newOrder = new NewOrder(
                customer,
                new NewOrderItem[1] { new NewOrderItem("ABC", 10, 10) },
                new NewOrderPayment[1] { new NewOrderPayment(10) })
            {
                Shipping = new NewOrderShipping(10, "ABC", address)
            };
        }
        #endregion

        [Fact]
        public void ExplicitOperator_ReturnMappedOrder_GiveValidNewOrder()
        {
            var sut = (Order)newOrder;

            Assert.Equal(newOrder.Code, sut.Code);
        }
    }
}
