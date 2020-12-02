using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using Moq;
using MundiPagg.Domain.CreateOrders.Entities.NewOrders;
using MundiPagg.Domain.CreateOrders.Interfaces;
using MundiPagg.Domain.CreateOrders.Services;
using MundiPagg.Domain.Orders.Entities.Orders;
using MundiPagg.Domain.Shared;
using System;
using Xunit;

namespace MundiPagg.Tests.Domain.OrderTest
{
    public class OrderServiceTest
    {
        #region [Objetcs]
        private readonly NewOrder _newOrder;

        private readonly Customer _customer;
        private readonly Item[] _item;

        public OrderServiceTest()
        {
            var birthdate = "1991-02-05";
            var type = "individual";
            var document = "11111111111";
            var email = "email@email.com";
            
            var address = new Address("29101200", "ABC", "ES", "BR", "012, DEF, GHF");
            var newOrderCustomer = new NewOrderCustomer(type, birthdate, document, email)
            {
                Address = address
            };

            _newOrder = new NewOrder(
                newOrderCustomer,
                new NewOrderItem[1] { new NewOrderItem("ABC", 10, 10) },
                new NewOrderPayment[1] { new NewOrderPayment(10) })
            {
                Shipping = new NewOrderShipping(10, "ABC", address)
            };


            _item = new Item[1];
            _customer = new Customer();
        }
        #endregion

        [Fact]
        public void GetOrder_ReturnOrder_GiveValidJobId()
        {
            long jobId = 1;
            var repositoryMock = new Mock<IOrderRepository>();
            var brokerMock = new Mock<IOrderBroker>();
            var loggerMock = new Mock<ILogger<OrderService>>();

            repositoryMock.Setup(x => x.GetOrder(It.IsAny<long>()))
                .Returns<long>((t) =>
                {
                    var getOrder = new Order(_item, _customer);
                    getOrder.SetJobId(t);
                    return getOrder;
                });

            var repository = repositoryMock.Object;
            var broker = brokerMock.Object;
            var logger = loggerMock.Object;

            var sut = new OrderService(repository, broker, logger);

            var order = sut.GetOrder(jobId);

            Assert.Equal(jobId, order.JobId);
        }

        [Fact]
        public void GetOrder_ReturnNull_GiveInvalidJobId()
        {
            var repositoryMock = new Mock<IOrderRepository>();
            var brokerMock = new Mock<IOrderBroker>();
            var loggerMock = new Mock<ILogger<OrderService>>();

            repositoryMock.Setup(x => x.GetOrder(It.IsAny<long>())).Returns(() => null);

            var repository = repositoryMock.Object;
            var broker = brokerMock.Object;
            var logger = loggerMock.Object;

            var sut = new OrderService(repository, broker, logger);

            var order = sut.GetOrder(It.IsAny<long>());

            Assert.Null(order);
        }

        [Fact]
        public void GetOrder_ReturnOrder_GiveValidCode()
        {
            string code = "ABC012";
            var repositoryMock = new Mock<IOrderRepository>();
            var brokerMock = new Mock<IOrderBroker>();
            var loggerMock = new Mock<ILogger<OrderService>>();

            repositoryMock.Setup(x => x.GetOrder(It.IsAny<string>()))
                .Returns<string>((t) => new Order(_item, _customer) { Code = t });

            var repository = repositoryMock.Object;
            var broker = brokerMock.Object;
            var logger = loggerMock.Object;

            var sut = new OrderService(repository, broker, logger);

            var order = sut.GetOrder(code);

            Assert.Equal(code, order.Code);
        }

        [Fact]
        public void GetOrder_ReturnNull_GiveInvalidCode()
        {
            var repositoryMock = new Mock<IOrderRepository>();
            var brokerMock = new Mock<IOrderBroker>();
            var loggerMock = new Mock<ILogger<OrderService>>();

            repositoryMock.Setup(x => x.GetOrder(It.IsAny<string>())).Returns(() => null);

            var repository = repositoryMock.Object;
            var broker = brokerMock.Object;
            var logger = loggerMock.Object;

            var sut = new OrderService(repository, broker, logger);

            var order = sut.GetOrder(It.IsAny<string>());

            Assert.Null(order);
        }

        [Fact]
        public void CreateNewOrder_ReturnOrder_GiveValidNewOrder()
        {
            var repositoryMock = new Mock<IOrderRepository>();
            var brokerMock = new Mock<IOrderBroker>();
            var loggerMock = new Mock<ILogger<OrderService>>();

            repositoryMock.Setup(x => x.GetNextId()).Returns(() => It.IsAny<long>());
            repositoryMock.Setup(x => x.Create(It.IsAny<Order>()));
            brokerMock.Setup(x => x.SendOrderMessage(It.IsAny<string>(), It.IsAny<string>()));

            var repository = repositoryMock.Object;
            var broker = brokerMock.Object;
            var logger = loggerMock.Object;

            var sut = new OrderService(repository, broker, logger);

            var order = sut.CreateNewOrder(_newOrder);

            Assert.Equal(_newOrder.Code, order.Code);
        }

        [Fact]
        public void CreateNewOrder_ReturnException_GiveInvalidNewOrder()
        {
            var repositoryMock = new Mock<IOrderRepository>();
            var brokerMock = new Mock<IOrderBroker>();
            var loggerMock = new Mock<ILogger<OrderService>>();

            repositoryMock.Setup(x => x.GetNextId()).Returns(() => It.IsAny<long>());
            repositoryMock.Setup(x => x.Create(It.IsAny<Order>()));
            brokerMock.Setup(x => x.SendOrderMessage(It.IsAny<string>(), It.IsAny<string>()));

            var repository = repositoryMock.Object;
            var broker = brokerMock.Object;
            var logger = loggerMock.Object;

            var sut = new OrderService(repository, broker, logger);

            Assert.Throws<ArgumentException>(() => sut.CreateNewOrder(null));
        }

        [Fact]
        public void CreateNewOrder_CallCreate_Once_GiveValidNewOrder()
        {
            var repositoryMock = new Mock<IOrderRepository>();
            var brokerMock = new Mock<IOrderBroker>();
            var loggerMock = new Mock<ILogger<OrderService>>();

            repositoryMock.Setup(x => x.GetNextId()).Returns(() => It.IsAny<long>());
            brokerMock.Setup(x => x.SendOrderMessage(It.IsAny<string>(), It.IsAny<string>()));

            var repository = repositoryMock.Object;
            var broker = brokerMock.Object;
            var logger = loggerMock.Object;

            var sut = new OrderService(repository, broker, logger);

            var order = sut.CreateNewOrder(_newOrder);

            repositoryMock.Verify(m => m.Create(order), Times.Once());
        }

        [Fact]
        public void CreateNewOrder_CallSendOrderMessage_Once_GiveValidNewOrder()
        {
            var repositoryMock = new Mock<IOrderRepository>();
            var brokerMock = new Mock<IOrderBroker>();
            var loggerMock = new Mock<ILogger<OrderService>>();
            var jsonTransform = new Mock<JsonTransform>();

            repositoryMock.Setup(x => x.GetNextId()).Returns(() => 1);
            brokerMock.Setup(x => x.SendOrderMessage(It.IsAny<string>(), It.IsAny<string>()));

            var repository = repositoryMock.Object;
            var broker = brokerMock.Object;
            var logger = loggerMock.Object;

            var sut = new OrderService(repository, broker, logger);

            var order = sut.CreateNewOrder(_newOrder);

            brokerMock.Verify(m => m.SendOrderMessage(It.IsAny<string>(), "1"), Times.Once());
        }

        [Fact]
        public void CreateNewOrder_ReturnOrderWithSameGenerateJobId_GiveValidNewOrder()
        {
            var repositoryMock = new Mock<IOrderRepository>();
            var brokerMock = new Mock<IOrderBroker>();
            var loggerMock = new Mock<ILogger<OrderService>>();

            repositoryMock.Setup(x => x.GetNextId()).Returns(() => 1);
            repositoryMock.Setup(x => x.Create(It.IsAny<Order>()));
            brokerMock.Setup(x => x.SendOrderMessage(It.IsAny<string>(), It.IsAny<string>()));

            var repository = repositoryMock.Object;
            var broker = brokerMock.Object;
            var logger = loggerMock.Object;

            var sut = new OrderService(repository, broker, logger);

            var order = sut.CreateNewOrder(_newOrder);

            Assert.Equal(1, order.JobId);
        }

        [Fact]
        public void UpdateProcessedOrder_CallUpdateOnce_GiveValidIdAndValidOrder()
        {
            long jobId = 1;
            Order order = new Order(_item, _customer);
            var objectId = ObjectId.GenerateNewId();

            Order getOrder = new Order(_item, _customer) { InternalId = objectId };

            var repositoryMock = new Mock<IOrderRepository>();
            var brokerMock = new Mock<IOrderBroker>();
            var loggerMock = new Mock<ILogger<OrderService>>();

            repositoryMock.Setup(x => x.Update(order));
            repositoryMock.Setup(x => x.GetOrder(jobId))
                .Returns<long>((l) => { getOrder.SetJobId(l); return getOrder; });

            var repository = repositoryMock.Object;
            var broker = brokerMock.Object;
            var logger = loggerMock.Object;

            var sut = new OrderService(repository, broker, logger);

            sut.UpdateProcessedOrder(jobId, order);

            repositoryMock.Verify(m => m.Update(order), Times.Once());
        }

        [Fact]
        public void UpdateProcessedOrder_CallGetOrder_GiveValidIdAndValidOrder()
        {
            long jobId = 1;
            Order order = new Order(_item, _customer);
            var objectId = ObjectId.GenerateNewId();

            Order getOrder = new Order(_item, _customer) { InternalId = objectId };

            var repositoryMock = new Mock<IOrderRepository>();
            var brokerMock = new Mock<IOrderBroker>();
            var loggerMock = new Mock<ILogger<OrderService>>();

            repositoryMock.Setup(x => x.Update(order));
            repositoryMock.Setup(x => x.GetOrder(jobId))
                .Returns<long>((l) => { getOrder.SetJobId(l); return getOrder; });

            var repository = repositoryMock.Object;
            var broker = brokerMock.Object;
            var logger = loggerMock.Object;

            var sut = new OrderService(repository, broker, logger);

            sut.UpdateProcessedOrder(jobId, order);

            repositoryMock.Verify(m => m.GetOrder(jobId), Times.Once());
        }

        [Fact]
        public void UpdateProcessedOrder_ReturnOrderWithSameGiveJobId_GiveValidIdAndValidOrder()
        {
            long jobId = 1;
            Order order = new Order(_item, _customer);
            var objectId = ObjectId.GenerateNewId();

            Order getOrder = new Order(_item, _customer) { InternalId = objectId };

            var repositoryMock = new Mock<IOrderRepository>();
            var brokerMock = new Mock<IOrderBroker>();
            var loggerMock = new Mock<ILogger<OrderService>>();

            repositoryMock.Setup(x => x.Update(order));
            repositoryMock.Setup(x => x.GetOrder(jobId))
                .Returns<long>((l) => { getOrder.SetJobId(l); return getOrder; });

            var repository = repositoryMock.Object;
            var broker = brokerMock.Object;
            var logger = loggerMock.Object;

            var sut = new OrderService(repository, broker, logger);

            sut.UpdateProcessedOrder(jobId, order);

            Assert.Equal(jobId, order.JobId);
        }

        [Fact]
        public void UpdateProcessedOrder_ReturnOrderWithSameGenerateObjectId_GiveValidIdAndValidOrder()
        {
            long jobId = 1;
            Order order = new Order(_item, _customer);
            var objectId = ObjectId.GenerateNewId();

            Order getOrder = new Order(_item, _customer) { InternalId = objectId };

            var repositoryMock = new Mock<IOrderRepository>();
            var brokerMock = new Mock<IOrderBroker>();
            var loggerMock = new Mock<ILogger<OrderService>>();

            repositoryMock.Setup(x => x.Update(order));
            repositoryMock.Setup(x => x.GetOrder(jobId))
                .Returns<long>((l) => { getOrder.SetJobId(l); return getOrder; });

            var repository = repositoryMock.Object;
            var broker = brokerMock.Object;
            var logger = loggerMock.Object;

            var sut = new OrderService(repository, broker, logger);

            sut.UpdateProcessedOrder(jobId, order);

            Assert.Equal(objectId, order.InternalId);
        }

        [Fact]
        public void UpdateProcessedOrder_ReturnNull_GiveInvalidId()
        {
            long jobId = 1;
            Order order = new Order(_item, _customer);
            var objectId = ObjectId.GenerateNewId();

            Order getOrder = new Order(_item, _customer) { InternalId = objectId };

            var repositoryMock = new Mock<IOrderRepository>();
            var brokerMock = new Mock<IOrderBroker>();
            var loggerMock = new Mock<ILogger<OrderService>>();

            repositoryMock.Setup(x => x.Update(order));
            repositoryMock.Setup(x => x.GetOrder(jobId))
                .Returns(() => null);

            var repository = repositoryMock.Object;
            var broker = brokerMock.Object;
            var logger = loggerMock.Object;

            var sut = new OrderService(repository, broker, logger);

            var response = sut.UpdateProcessedOrder(jobId, order);

            Assert.Null(response);
        }
    }
}
