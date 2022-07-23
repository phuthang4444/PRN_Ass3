using BusinessObejct.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class OrderRepository : IOrderRepository
    {
        public void DeleteOrder(int orderID) => OrderDAO.Instance.Remove(orderID);

        public Order GetOrderByID(int orderID) => OrderDAO.Instance.GetOrderByID(orderID);

        public IEnumerable<Order> GetOrders() => OrderDAO.Instance.GetOrderList();

        public void InsertOrder(Order order) => OrderDAO.Instance.AddNew(order);

        public IEnumerable<Order> Search(string searchValue) => OrderDAO.Instance.Search(searchValue);

        public void UpdateOrder(Order order) => OrderDAO.Instance.Update(order);
    }
}
