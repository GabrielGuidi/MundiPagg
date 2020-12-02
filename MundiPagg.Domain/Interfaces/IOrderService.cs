using MundiPagg.Domain.CreateOrders.Entities.NewOrders;
using MundiPagg.Domain.Orders.Entities.Orders;

namespace MundiPagg.Domain.CreateOrders.Interfaces
{
    public interface IOrderService
    {
        Order CreateNewOrder(NewOrder order);
        Order GetOrder(string code);
        Order GetOrder(long id);
        Order UpdateProcessedOrder(long id, Order order);
    }
}
