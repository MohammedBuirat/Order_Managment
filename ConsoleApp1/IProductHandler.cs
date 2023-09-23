using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ConsoleApp1.Enums.Enums;

namespace Order_Managment
{
    internal interface IProductHandler
    {
        public OperationResult AddProduct(Product product);

        public List<Product> GetAllProducts();

        public OperationResult EditProduct(int id, Product newProduct);

        public OperationResult DeleteProduct(int id);

        public Product GetProductById(int id);

        public Product GetProductByName(string name);
    }
}
