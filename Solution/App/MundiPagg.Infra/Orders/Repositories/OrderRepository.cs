using MongoDB.Bson;
using MongoDB.Driver;
using MundiPagg.Domain.CreateOrders.Interfaces;
using MundiPagg.Domain.Orders.Entities.Orders;
using MundiPagg.Infra.Shared.Interfaces;

namespace MundiPagg.Infra.Orders.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IMundiContext _context;

        public OrderRepository(IMundiContext context)
        {
            _context = context;
        }

        public Order GetOrder(long id)
        {
            FilterDefinition<Order> filter = Builders<Order>.Filter.Eq(m => m.JobId, id);
            return _context
                    .Orders
                    .Find(filter)
                    .FirstOrDefault();
        }

        public Order GetOrder(string code)
        {
            FilterDefinition<Order> filter = Builders<Order>.Filter.Eq(m => m.Code, code);
            return _context
                    .Orders
                    .Find(filter)
                    .FirstOrDefault();
        }

        public void Create(Order order)
        {
            _context.Orders.InsertOne(order);
        }

        public bool Update(Order order)
        {
            ReplaceOneResult updateResult =
                _context
                .Orders
                .ReplaceOne(
                    filter: g => g.JobId == order.JobId,
                    replacement: order);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        public long GetNextId()
        {
            return _context.Orders.CountDocuments(new BsonDocument()) + 1;
        }
    }
}
