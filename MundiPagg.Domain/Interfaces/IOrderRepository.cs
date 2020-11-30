using MundiPagg.Domain.Orders.Entities.Orders;

namespace MundiPagg.Domain.CreateOrders.Interfaces
{
    public interface IOrderRepository
    {
        Order GetOrder(long id);
        Order GetOrder(string code);
        void Create(Order order);
        bool Update(Order order);
        long GetNextId();
    }
}
