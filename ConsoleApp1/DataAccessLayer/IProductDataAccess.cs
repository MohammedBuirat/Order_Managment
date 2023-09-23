using Order_Managment;

namespace ConsoleApp1.DataAccessLayer
{
    internal interface IProductDataAccess
    {
        public bool ProductExistById(int id);

        public bool ProductExistByName(string name);

        public Product FindProductById(int id);

        public Product FindProductByName(string name);

        public List<Product> GetAllProducts();

        public bool UpdateProduct(int id, Product product);

        public bool DeleteProduct(int id);

        public bool AddProduct(Product product);
    }
}
