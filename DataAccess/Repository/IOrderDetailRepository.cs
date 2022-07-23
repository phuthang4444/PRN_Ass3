using BusinessObejct.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IOrderDetailRepository
    {
        IEnumerable<OrderDetail> GetOrderDetails();
        IEnumerable<OrderDetail> GetOrderDetailsByOrderID(int orderID);
        OrderDetail GetOrderDetailByOrderID(int orderID, int productID);
        IEnumerable<OrderDetail> Search(string searchValue);
        void InsertOrderDetail(OrderDetail detail);
        void DeleteOrderDetail(int orderID, int productID);
        void UpdateOrderDetail(OrderDetail detail);
    }
}
