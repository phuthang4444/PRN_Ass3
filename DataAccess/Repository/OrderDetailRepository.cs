using BusinessObejct.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        public void DeleteOrderDetail(int orderID, int productID) => OrderDetailDAO.Instance.Remove(orderID, productID);

        public OrderDetail GetOrderDetailByOrderID(int orderID, int productID) => OrderDetailDAO.Instance.GetOrderDetailByOrderID(orderID, productID);

        public IEnumerable<OrderDetail> GetOrderDetails() => OrderDetailDAO.Instance.GetOrderDetailList();

        public IEnumerable<OrderDetail> GetOrderDetailsByOrderID(int orderID) => OrderDetailDAO.Instance.GetOrderDetailsByOrderID(orderID);

        public void InsertOrderDetail(OrderDetail detail) => OrderDetailDAO.Instance.AddNew(detail);

        public IEnumerable<OrderDetail> Search(string searchValue) => OrderDetailDAO.Instance.Search(searchValue);

        public void UpdateOrderDetail(OrderDetail detail) => OrderDetailDAO.Instance.Update(detail);
    }
}
