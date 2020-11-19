using MundiPagg.Domain.CreateOrders.Entities;
using MundiPagg.Domain.CreateOrders.Interfaces;
using System;

namespace MundiPagg.Infra.Orders.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        public void Insert(Order order)
        {
            Console.WriteLine("Gravou no banco!");
        }
    }
}
