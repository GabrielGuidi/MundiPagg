using MundiPagg.Domain.CreateOrders.Entities;

namespace MundiPagg.Domain.CreateOrders.Interfaces
{
    public interface IOrderService
    {
        Order CreateNewOrder(Order order);
    }
}
