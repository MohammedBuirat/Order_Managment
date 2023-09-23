using Order_Managment;

namespace ConsoleApp1.DataAccessLayer.MongoAccess
{
    internal class MongoProductAccess : IProductDataAccess
    {
        public bool AddProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public bool DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }

        public Product FindProductById(int id)
        {
            throw new NotImplementedException();
        }

        public Product FindProductByName(string name)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAllProducts()
        {
            throw new NotImplementedException();
        }

        public bool ProductExistById(int id)
        {
            throw new NotImplementedException();
        }

        public bool ProductExistByName(string name)
        {
            throw new NotImplementedException();
        }

        public bool UpdateProduct(int id, Product product)
        {
            throw new NotImplementedException();
        }
    }
}