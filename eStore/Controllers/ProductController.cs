using BusinessObejct.Object;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eStore.Controllers
{
    public class ProductController : Controller
    {
        IProductRepository productRepository = null;
        public ProductController() => productRepository = new ProductRepository();


        // GET: ProductController
        public ActionResult Index()
        {
            var adminID = HttpContext.Session.GetInt32("ID");
            if (adminID != 1)
            {
                return NotFound();
            }

            var products = productRepository.GetProducts();
            return View(products);
        }

        // GET: ProductController/Details/5
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
            var product = productRepository.GetProductById(id.Value);
            if (product == null) 
            {
                return NotFound();
            }
            return View(product);
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            var adminID = HttpContext.Session.GetInt32("ID");
            if (adminID != 1)
            {
                return NotFound();
            }

            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
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
                    productRepository.InsertProduct(product);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(product);
            }
        }

        // GET: ProductController/Edit/5
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
            var product = productRepository.GetProductById(id.Value);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Product product)
        {
            var adminID = HttpContext.Session.GetInt32("ID");
            if (adminID != 1)
            {
                return NotFound();
            }

            try
            {
                if (id != product.ProductId)
                {
                    return NotFound();
                }
                if (ModelState.IsValid)
                {
                    productRepository.UpdateProduct(product);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        // GET: ProductController/Delete/5
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
            var product = productRepository.GetProductById(id.Value);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: ProductController/Delete/5
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
                productRepository.DeleteProduct(id);
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
