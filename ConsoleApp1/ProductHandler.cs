using ConsoleApp1.DataAccessLayer;
using Order_Managment;
using static ConsoleApp1.Enums.Enums;

namespace ConsoleApp1
{
    internal class ProductHandler : IProductHandler
    {
        IProductDataAccess ProductDataAccess;

        public ProductHandler(IProductDataAccess dataAccess)
        {
            ProductDataAccess = dataAccess;
        }

        public OperationResult AddProduct(Product product)
        {
            OperationResult result = new OperationResult();
            bool ProductWasAdded = ProductDataAccess.AddProduct(product);
            if (ProductWasAdded)
            {
                result = OperationResult.Success;
            }
            else
            {
                result = OperationResult.OperationFailed;
            }
            return result;
        }

        public OperationResult DeleteProduct(int id)
        {
            OperationResult result = new OperationResult();
            if (!ProductDataAccess.ProductExistById(id))
            {
                result = OperationResult.ProductDoesNotExists;
                return result;
            }
            bool productWasDeleted = ProductDataAccess.DeleteProduct(id);
            if (productWasDeleted)
            {
                result = OperationResult.Success;
            }
            else
            {
                result = OperationResult.OperationFailed;
            }
            return result;
        }

        public OperationResult EditProduct(int id, Product newProduct)
        {
            OperationResult result = new OperationResult();
            if (!ProductDataAccess.ProductExistById(id))
            {
                result = OperationResult.ProductDoesNotExists;
                return result;
            }
            bool productWasUpdated = ProductDataAccess.UpdateProduct(id, newProduct);
            if (productWasUpdated)
            {
                result = OperationResult.Success;
            }
            else
            {
                result = OperationResult.OperationFailed;
            }
            return result;
        }

        public List<Product> GetAllProducts()
        {
            return ProductDataAccess.GetAllProducts();
        }

        public Product GetProductById(int id)
        {
            return ProductDataAccess.FindProductById(id);
        }

        public Product GetProductByName(string name)
        {
            return ProductDataAccess.FindProductByName(name);
        }
    }
}