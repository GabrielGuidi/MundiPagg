using MundiPagg.AppService.OrderApplicationServices.ValueObjects;
using MundiPagg.AppService.Shared;
using System;
using Xunit;

namespace MundiPagg.Tests.Domain.OrderTest
{
    public class TranslateOrderTest
    {
        [Fact]
        public void ExplicitOperator_ReturnMappedOrder_GiveValidNewOrder()
        {
            var countEnum = Enum.GetValues(typeof(OrderContentFormatEnum)).Length;
            var countDict = TranslateOrder.InstantiateFormatsDictonary().Count;

            Assert.Equal(countDict, countEnum);
        }
    }
}
