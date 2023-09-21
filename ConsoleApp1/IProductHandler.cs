using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_Managment
{
    internal interface IProductHandler
    {
        public bool AddProduct(Product product);

        public List<Product> GetAllProducts();

        public bool EditProduct(int id, Product newProduct);

        public bool DeleteProduct(int id);

        public Product GetProductById(int id);

        public Product GetProductByName(string name);
    }
}
