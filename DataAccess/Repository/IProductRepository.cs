using BusinessObejct.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts();
        Product GetProductById(int productID);
        void InsertProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int productID);
        IEnumerable<Product> Search(string searchValue);
    }
}
