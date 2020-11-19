using MundiPagg.AppService.DataTransfer;

namespace MundiPagg.AppService.OrderApplicationServices.Interfaces
{
    public interface IOrderAppService
    {
        OrderResponse CreateNewOrder(OrderRequest orderRequest);
    }
}
