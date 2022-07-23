using BusinessObejct.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetOrders();
        Order GetOrderByID(int orderID);
        void InsertOrder(Order order);
        void UpdateOrder(Order order);
        void DeleteOrder(int orderID);
        IEnumerable<Order> Search(string searchValue);
    }
}
