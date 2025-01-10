using ConsoleApp10.Models;
using System.Collections.Generic;

namespace ConsoleApp10.Interfaces
{
    public interface IOrderRepository
    {
        void PlaceOrder(Order order);
        IEnumerable<Order> GetOrders();
        Order GetOrderById(int orderId);
    }
}
