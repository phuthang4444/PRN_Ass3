using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObejct;
using BusinessObejct.Object;

namespace DataAccess
{
    public class OrderDetailDAO
    {
        private static OrderDetailDAO instance = null;
        private static readonly object instanceLock = new object();
        public static OrderDetailDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new OrderDetailDAO();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<OrderDetail> GetOrderDetailList()
        {
            List<OrderDetail> orderDetails = new List<OrderDetail>();
            try
            {
                using (var context = new SaleManagementContext())
                {
                    orderDetails = context.OrderDetails.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return orderDetails;
        }

        public IEnumerable<OrderDetail> GetOrderDetailsByOrderID(int orderID)
        {
            List<OrderDetail> orderDetail = new List<OrderDetail>();
            try
            {
                using (var context = new SaleManagementContext())
                {
                    orderDetail = context.OrderDetails.ToList().FindAll(m => m.OrderId == orderID);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return orderDetail;
        }

        public OrderDetail GetOrderDetailByOrderID(int orderID, int productID)
        {
            OrderDetail orderDetail = null;
            try
            {
                using (var context = new SaleManagementContext())
                {
                    orderDetail = context.OrderDetails.SingleOrDefault(m => m.OrderId == orderID && m.ProductId == productID);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return orderDetail;
        }

        public void AddNew(OrderDetail orderDetail)
        {
            try
            {
                OrderDetail _orderDetail = OrderDetailDAO.Instance.GetOrderDetailByOrderID(orderDetail.OrderId, orderDetail.ProductId);
                if (_orderDetail == null)
                {
                    using (var context = new SaleManagementContext())
                    {
                        context.OrderDetails.Add(orderDetail);
                        context.SaveChanges();
                    }
                }
                else throw new Exception("The orderDetail is already existed!");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(OrderDetail orderDetail)
        {
            try
            {
                OrderDetail _orderDetail = OrderDetailDAO.Instance.GetOrderDetailByOrderID(orderDetail.OrderId, orderDetail.ProductId);
                if (_orderDetail != null)
                {
                    using (var context = new SaleManagementContext())
                    {
                        context.OrderDetails.Update(orderDetail);
                        context.SaveChanges();
                    }
                }
                else throw new Exception("The orderDetail does not exist!");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Remove(int orderID, int productID)
        {
            try
            {
                OrderDetail orderDetail = OrderDetailDAO.Instance.GetOrderDetailByOrderID(orderID, productID);
                if (orderDetail != null)
                {
                    using (var context = new SaleManagementContext())
                    {
                        context.OrderDetails.Remove(orderDetail);
                        context.SaveChanges();

                    }
                }
                else throw new Exception("The orderDetail does not exist!");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IEnumerable<OrderDetail> Search(string searchValue)
        {
            var result = new List<OrderDetail>();
            try
            {
                using (var context = new SaleManagementContext())
                {
                    var orderDetail = from o in context.OrderDetails
                                  where o.ProductId.ToString().Contains(searchValue) || o.OrderId.ToString().Contains(searchValue)
                                  select o;
                    result = orderDetail.ToList<OrderDetail>();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
    }
}
