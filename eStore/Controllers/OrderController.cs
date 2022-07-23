using BusinessObejct.Object;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace eStore.Controllers
{
    public class OrderController : Controller
    {
        IOrderRepository orderRepository = null;
        IOrderDetailRepository orderDetailRepository = null;
        public OrderController()
        {
            orderRepository = new OrderRepository();
            orderDetailRepository = new OrderDetailRepository();
        }

        // GET: OrderController
        public ActionResult Index()
        {
            var adminID = HttpContext.Session.GetInt32("ID");
            if (adminID != 1) 
            { 
                return NotFound(); 
            }

            var orders = orderRepository.GetOrders();
            return View(orders);
        }

        // GET: OrderController/Details/5
        public ActionResult Details(int? id)
        {
            var adminID = HttpContext.Session.GetInt32("ID");
            if (adminID != 1)
            {
                return NotFound();
            }
            if (id == null) 
            { 
                return NotFound(); 
            }
            var order = orderRepository.GetOrderByID(id.Value);
            if (order == null) 
            { 
                return NotFound(); 
            }
            return View(order);
        }

        // GET: OrderController/Create
        public ActionResult Create()
        {
            var adminID = HttpContext.Session.GetInt32("ID");
            if (adminID != 1) 
            { 
                return NotFound(); 
            }
            return View();
        }

        // POST: OrderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Order order)
        {
            var adminID = HttpContext.Session.GetInt32("ID");
            if (adminID != 1)
            {
                return NotFound();
            }
            try
            {
                if (ModelState.IsValid)
                {
                    orderRepository.InsertOrder(order);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(order);
            }
        }

        // GET: OrderController/Edit/5
        public ActionResult Edit(int? id)
        {
            var adminID = HttpContext.Session.GetInt32("ID");
            if (adminID != 1)
            {
                return NotFound();
            }

            if (id == null)
            {
                return NotFound();
            }
            var order = orderRepository.GetOrderByID(id.Value);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: OrderController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Order order)
        {
            var adminID = HttpContext.Session.GetInt32("ID");
            if (adminID != 1)
            {
                return NotFound();
            }
            try
            {
                if (id != order.OrderId)
                {
                    return NotFound();
                }
                if (ModelState.IsValid) 
                {
                    orderRepository.UpdateOrder(order);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        // GET: OrderController/Delete/5
        public ActionResult Delete(int? id)
        {
            var adminID = HttpContext.Session.GetInt32("ID");
            if (adminID != 1) 
            {
                return NotFound();
            }

            if (id == null)
            {
                return NotFound();
            }
            var order = orderRepository.GetOrderByID(id.Value);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: OrderController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var adminID = HttpContext.Session.GetInt32("ID");
            if (adminID != 1) 
            { 
                return NotFound(); 
            }
            try
            {
                orderRepository.DeleteOrder(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }
    }
}
