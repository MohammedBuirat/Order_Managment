using Serilog;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace ConsoleApp1.DataAccessLayer.SQLAcess
{
    internal class SQLProductAccess : IProductRepository
    {

        string connectionString = ConfigurationManager.ConnectionStrings["SqlServerConnection"].ConnectionString;

        public bool AddProduct(Product product)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO Products (Name, Price, Quantity) VALUES (@ProductName, @Price, @Quantity)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProductName", product.Name);
                        command.Parameters.AddWithValue("@Price", product.Price);
                        command.Parameters.AddWithValue("@Quantity", product.Quantity);
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public bool DeleteProduct(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = $"DELETE FROM Products WHERE Id = {id}";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);

                        int rowsAffected = command.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString()));
                return false;
            }
        }

        public Product FindProductById(int id)
        {
            try
            {
                Product product = null;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = $"SELECT * FROM Products WHERE Id = {id}";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int productId = reader.GetInt32(0);
                                string productName = reader.GetString(1);
                                decimal productPrice = reader.GetDecimal(2);
                                int productQuantity = reader.GetInt32(3);
                                product = new Product(productId, productName, productPrice, productQuantity);
                            }
                        }
                    }
                }
                return product;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        public Product FindProductByName(string name)
        {
            try
            {
                Product product = null;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = $"SELECT * FROM Products WHERE Name = '{name}'";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int productId = reader.GetInt32(0);
                                string productName = reader.GetString(1);
                                decimal productPrice = reader.GetDecimal(2);
                                int productQuantity = reader.GetInt32(3);
                                product = new Product(productId, productName, productPrice, productQuantity);
                            }
                        }
                    }
                }
                return product;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        public List<Product> GetAllProducts()
        {
            try
            {
                List<Product> productList = new List<Product>();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM Products";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int productId = reader.GetInt32(0);
                                string productName = reader.GetString(1);
                                decimal productPrice = reader.GetDecimal(2);
                                int productQuantity = reader.GetInt32(3);
                                Product product = new Product(productId, productName, productPrice, productQuantity);
                                productList.Add(product);
                            }
                        }
                    }
                }
                return productList;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        public bool ProductExistById(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = $"SELECT * FROM Products WHERE Id = {id}";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            return reader.Read();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }

        public bool ProductExistByName(string name)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = $"SELECT * FROM Products WHERE Name = '{name}'";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            return reader.Read();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }

        public bool UpdateProduct(int id, Product product)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "UPDATE Products " +
                                   "SET Name = @New_Name, " +
                                   "    Price = @New_Price, " +
                                   "    Quantity = @New_Quantity " + // Corrected parameter name
                                   "WHERE Id = @Id"; // Corrected parameter name

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        command.Parameters.AddWithValue("@New_Name", product.Name);
                        command.Parameters.AddWithValue("@New_Price", product.Price);
                        command.Parameters.AddWithValue("@New_Quantity", product.Quantity); // Corrected parameter name
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }
    }
}
}
