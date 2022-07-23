using BusinessObejct.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ProductRepository : IProductRepository
    {
        public void DeleteProduct(int productID) => ProductDAO.Instance.Remove(productID);

        public Product GetProductById(int productID) => ProductDAO.Instance.GetProductByID(productID);

        public IEnumerable<Product> GetProducts() => ProductDAO.Instance.GetProductList();

        public void InsertProduct(Product product) => ProductDAO.Instance.AddNew(product);

        public IEnumerable<Product> Search(string searchValue) => ProductDAO.Instance.Search(searchValue);

        public void UpdateProduct(Product product) => ProductDAO.Instance.Update(product);
    }
}
