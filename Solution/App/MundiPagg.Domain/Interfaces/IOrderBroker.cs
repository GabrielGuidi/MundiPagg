namespace MundiPagg.Domain.CreateOrders.Interfaces
{
    public interface IOrderBroker
    {
        void SendOrderMessage(string json, string id);
    }
}
