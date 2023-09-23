using ConsoleApp1;
using ConsoleApp1.DataAccessLayer;
using System.Data.SqlTypes;
using static ConsoleApp1.Enums.Enums;

namespace Order_Managment
{
    internal class Program
    {
        static IProductHandler handler;
        static void Main(string[] args)
        {
            handler = new ProductHandler(new SQLProductAccess());
            PickOperation();
        }

        static void PickOperation()
        {
            while (true)
            {
                Console.WriteLine("*********************************************");
                Console.WriteLine("*********************************************");
                Console.WriteLine("*********************************************");
                Console.WriteLine("Please pick one of the following options");
                Console.WriteLine("1)Print all products.");
                Console.WriteLine("2)Search Product by name.");
                Console.WriteLine("3)Search Product by ID.");
                Console.WriteLine("4)Delete a Product.");
                Console.WriteLine("5)Update Product.");
                Console.WriteLine("6)Add a Product");
                Console.WriteLine("7)Exit.");
                string userInput = Console.ReadLine();

                if (int.TryParse(userInput, out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            PrintAllProducts();
                            break;

                        case 2:
                            SearchProductByName();
                            break;

                        case 3:
                            SearchProductById();
                            break;

                        case 4:
                            DeleteProduct();
                            break;

                        case 5:
                            UpdateProduct();
                            break;

                        case 6:
                            AddNewProduct();
                            break;

                        case 7:
                            Environment.Exit(0);
                            break;

                        default:
                            Console.WriteLine("Invalid option. Please choose a valid option (1-6).");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }

            }
        }

        static void PrintResultOfOperation(OperationResult result)
        {
            if (result == OperationResult.Success)
            {
                Console.WriteLine("Opeartion was made sucessfully.");
            }
            else if (result == OperationResult.ConnectionError)
            {
                Console.WriteLine("Opeartion Failed, There was a problem with the connection.");
            }
            else if (result == OperationResult.ProductDoesNotExists)
            {
                Console.WriteLine("Operation Failed, there wehre no product with the given id.");
            }
            else if (result == OperationResult.OperationFailed)
            {
                Console.WriteLine("Opeartion Failed");
            }
            else if(result == OperationResult.ProductNameIsTaken)
            {
                Console.WriteLine("The given name is alredy taken");
            }
        }

        static void PrintAllProducts()
        {
            List<Product> products = handler.GetAllProducts();
            if (products == null || products.Count() == 0)
            {
                Console.WriteLine("There is no stored products");
            }
            foreach (var product in products)
            {
                Console.WriteLine(product.ToString());
            }
        }

        static void SearchProductByName()
        {
            string name = Console.ReadLine();
            Product product = handler.GetProductByName(name);
            if (product != null)
            {
                Console.WriteLine(product.ToString());
            }
            else
            {
                Console.WriteLine("There is no product with this name");
            }
        }

        static void SearchProductById()
        {
            Console.WriteLine("Please enter the id of the product");
            int id = int.Parse(Console.ReadLine());
            Product product = handler.GetProductById(id);
            if (product != null)
            {
                Console.WriteLine(product.ToString());
            }
            else
            {
                Console.WriteLine("There is no product with this name");
            }
        }

        static void DeleteProduct()
        {
            Console.WriteLine("Please enter the id of the product that you want to delete");
            int id = int.Parse(Console.ReadLine());
            OperationResult result = handler.DeleteProduct(id);
            Console.Write("Delete ");
            PrintResultOfOperation(result);
        }

        static void UpdateProduct()
        {
            Console.WriteLine("Please enter the id of the product that you want to update");
            int id = int.Parse(Console.ReadLine());
            Product oldProduct = handler.GetProductById(id);
            if (oldProduct == null)
            {
                Console.WriteLine("There is no product with this id");
                return;
            }
            Console.WriteLine(oldProduct.ToString());
            Console.WriteLine("Please enter the new name for the product " + id);
            string newName = Console.ReadLine();
            Console.WriteLine("Please enter the new price for the product " + id);
            SqlMoney newPrice = SqlMoney.Parse(Console.ReadLine());
            Console.WriteLine("Please enter the new quantity for the product " + id);
            int newQuantity = int.Parse(Console.ReadLine());
            Product newProdcut = new Product(oldProduct.Id, newName, newPrice, newQuantity);
            OperationResult result = handler.EditProduct(id, newProdcut);
            Console.Write("Update ");
            PrintResultOfOperation(result);
        }

        static void AddNewProduct()
        {
            Console.WriteLine("Please enter the product name");
            string name = Console.ReadLine();
            Console.WriteLine("Please enter the product price");
            SqlMoney price = SqlMoney.Parse(Console.ReadLine());
            Console.WriteLine("Please enter the product quantity");
            int quantity = int.Parse(Console.ReadLine());
            Product newProduct = new Product(-1, name, price, quantity);
            OperationResult result = handler.AddProduct(newProduct);
            Console.Write("Adding a new product ");
            PrintResultOfOperation(result);
        }
    }
}