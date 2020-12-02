using MundiPagg.Domain.CreateOrders.Entities.NewOrders;
using MundiPagg.Domain.Orders.Entities.Orders;
using System;
using Xunit;

namespace MundiPagg.Tests.Domain.OrderTest
{
    public class NewOrderCustomerTest
    {
        #region [Objetcs]
        private readonly NewOrderCustomer newOrderCustomer;

        public NewOrderCustomerTest()
        {
            var birthdate = "1991-02-05";
            var type = "individual";
            var document = "11111111111";
            var email = "email@email.com";

            newOrderCustomer = new NewOrderCustomer(type, birthdate, document, email);
        }
        #endregion

        [Fact]
        public void ExplicitOperator_ReturnMappedOrder_GiveValidNewOrder()
        {
            newOrderCustomer.Name = "email@email.com";
            newOrderCustomer.Address = new Address("123456", "ABC", "ES", "BR", "12, ABC, DEF");

            newOrderCustomer.SetEmail("email@email.com");
            newOrderCustomer.SetDocument("01234567890");
            newOrderCustomer.SetType("individual");
            newOrderCustomer.SetBirthdate("01/01/2020");

            var sut = (Customer)newOrderCustomer;

            Assert.Equal(newOrderCustomer.Document, sut.Document);
        }

        [Theory]
        [InlineData("01234567890")]
        [InlineData("01234567890123")]
        public void SetDocument_GiveValidDocument(string document)
        {
            newOrderCustomer.SetDocument(document);

            Assert.Equal(document, newOrderCustomer.Document);
        }

        [Theory]
        [InlineData("0123456789")]
        [InlineData("012345678912345")]
        [InlineData("012345678901")]
        public void SetDocument_ThrowApplicationException_GiveInvalidDocument(string document)
        {
            Assert.Throws<ApplicationException>(() => newOrderCustomer.SetDocument(document));
        }

        [Fact]
        public void SetEmail_GiveValidEmail()
        {
            var email = "email@email.com";
            newOrderCustomer.SetEmail(email);

            Assert.Equal(email, newOrderCustomer.Email);
        }

        [Fact]
        public void SetEmail_ThrowApplicationException_GiveInvalidEmail()
        {
            Assert.Throws<ApplicationException>(() => newOrderCustomer.SetEmail("notEmail"));
        }

        [Theory]
        [InlineData("individual")]
        [InlineData("company")]
        public void SetType_GiveValidType(string SetType)
        {
            newOrderCustomer.SetType(SetType);

            Assert.Equal(SetType, newOrderCustomer.Type);
        }

        [Fact]
        public void SetBirthdate_ThrowApplicationException_GiveInvalidType()
        {
            Assert.Throws<ApplicationException>(() => newOrderCustomer.SetType("notType"));
        }

        [Fact]
        public void SetBirthdate_GiveValidBirthdate()
        {
            var birthdate = "01/01/2020";
            newOrderCustomer.SetBirthdate(birthdate);

            Assert.Equal(birthdate, newOrderCustomer.Birthdate);
        }

        [Theory]
        [InlineData("01-01-20200")]
        [InlineData("20200101")]
        public void SetType_ThrowApplicationException_GiveInvalidBirthdate(string birthdate)
        {
            Assert.Throws<ApplicationException>(() => newOrderCustomer.SetBirthdate(birthdate));
        }
    }
}
