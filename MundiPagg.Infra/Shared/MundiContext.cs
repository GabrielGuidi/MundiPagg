using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MundiPagg.Domain.Orders.Entities.Orders;
using MundiPagg.Infra.Shared.Interfaces;

namespace MundiPagg.Infra.Shared
{
    public class MundiContext : IMundiContext
    {
        private readonly IMongoDatabase _db;
        
        public MundiContext(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("MundiPaggtDBConnectionString"));

            _db = client.GetDatabase(config.GetSection("MongoDB").GetSection("Database").Value);
        }

        public IMongoCollection<Order> Orders => _db.GetCollection<Order>("Orders");
    }
}
