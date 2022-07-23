using BusinessObejct.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class OrderDAO
    {
        private static OrderDAO instance = null;
        private static readonly object instanceLock = new object();
        public static OrderDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null) 
                    { 
                        instance = new OrderDAO(); 
                    }
                    return instance;
                }
            }
        }
        public IEnumerable<Order> GetOrderList()
        {
            List<Order> orders = new List<Order>();
            try
            {
                using (var context = new SaleManagementContext())
                {
                    orders = context.Orders.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return orders;
        }

        public Order GetOrderByID(int orderID)
        {
            Order order = null;
            try
            {
                using (var context = new SaleManagementContext())
                {
                    order = context.Orders.SingleOrDefault(m => m.OrderId == orderID);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return order;
        }
        public void AddNew(Order order)
        {
            try
            {
                Order _order = OrderDAO.Instance.GetOrderByID(order.OrderId);
                if (_order == null)
                {
                    using (var context = new SaleManagementContext())
                    {
                        context.Orders.Add(order);
                        context.SaveChanges();
                    }
                }
                else 
                { 
                    throw new Exception("The order is already existed!"); 
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(Order order)
        {
            try
            {
                Order _order = OrderDAO.Instance.GetOrderByID(order.OrderId);
                if (_order != null)
                {
                    using (var context = new SaleManagementContext())
                    {
                        context.Orders.Update(order);
                        context.SaveChanges();
                    }
                }
                else 
                { 
                    throw new Exception("The order does not exist!"); 
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Remove(int orderId)
        {
            try
            {
                Order order = OrderDAO.Instance.GetOrderByID(orderId);
                if (order != null)
                {
                    using (var context = new SaleManagementContext())
                    {
                        context.Orders.Remove(order);
                        context.SaveChanges();

                    }
                }
                else 
                { 
                    throw new Exception("The order does not exist!"); 
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IEnumerable<Order> Search(string searchValue)
        {
            var result = new List<Order>();
            try
            {
                using (var context = new SaleManagementContext())
                {
                    var Orders = from o in context.Orders
                                 where o.MemberId.ToString().Contains(searchValue) || o.OrderId.ToString().Contains(searchValue)
                                 select o;
                    result = Orders.ToList<Order>();
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
