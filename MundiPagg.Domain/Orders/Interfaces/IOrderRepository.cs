using MundiPagg.Domain.CreateOrders.Entities;

namespace MundiPagg.Domain.CreateOrders.Interfaces
{
    public interface IOrderRepository
    {
        void Insert(Order order);
    }
}
