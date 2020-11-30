using MongoDB.Driver;
using MundiPagg.Domain.Orders.Entities.Orders;

namespace MundiPagg.Infra.Shared.Interfaces
{
    public interface IMundiContext
    {
        IMongoCollection<Order> Orders { get; }
    }
}
