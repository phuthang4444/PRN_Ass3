using BusinessObejct.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ProductDAO
    {
        private static ProductDAO instance = null;
        private static readonly object instanceLock = new object();
        public static ProductDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null) 
                    { 
                        instance = new ProductDAO(); 
                    }
                    return instance;
                }
            }
        }
        public IEnumerable<Product> GetProductList()
        {
            var products = new List<Product>();
            try
            {
                using (var context = new SaleManagementContext())
                {
                    products = context.Products.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return products;
        }

        public Product GetProductByID(int productID)
        {
            Product product = null;
            try
            {
                using (var context = new SaleManagementContext())
                {
                    product = context.Products.SingleOrDefault(m => m.ProductId == productID);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return product;
        }

        public void AddNew(Product product)
        {
            try
            {
                Product _product = ProductDAO.Instance.GetProductByID(product.ProductId);
                if (_product == null)
                {
                    using (var context = new SaleManagementContext())
                    {
                        context.Products.Add(product);
                        context.SaveChanges();
                    }
                }
                else
                {
                    throw new Exception("The product is already existed!");
                }    
                    
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(Product product)
        {
            try
            {
                Product _product = ProductDAO.Instance.GetProductByID(product.ProductId);
                if (_product != null)
                {
                    using (var context = new SaleManagementContext())
                    {
                        context.Products.Update(product);
                        context.SaveChanges();
                    }
                }
                else throw new Exception("The product does not exist!");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Remove(int productId)
        {
            try
            {
                Product product = ProductDAO.Instance.GetProductByID(productId);
                if (product != null)
                {
                    using (var context = new SaleManagementContext())
                    {
                        context.Products.Remove(product);
                        context.SaveChanges();

                    }
                }
                else throw new Exception("The product does not exist!");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Product> Search(string searchValue)
        {
            var result = new List<Product>();
            try
            {
                using (var context = new SaleManagementContext())
                {
                    var Products = from p in context.Products
                                   where p.ProductId.ToString().Contains(searchValue) || p.ProductName.ToString().Contains(searchValue)
                                   select p;
                    result = Products.ToList<Product>();
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
