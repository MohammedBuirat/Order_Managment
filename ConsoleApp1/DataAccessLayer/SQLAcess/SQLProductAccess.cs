using Order_Managment;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace ConsoleApp1.DataAccessLayer.SQLAcess
{
    internal class SQLProductAccess : IProductDataAccess
    {

        string connectionString = "Data Source=M7mad;Initial Catalog=Order_Managment;Integrated Security=True;TrustServerCertificate=True";

        public bool AddProduct(Product product)
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

        public bool DeleteProduct(int id)
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

        public Product FindProductById(int id)
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
                            SqlMoney productPrice = reader.GetSqlMoney(2);
                            int productQuantity = reader.GetInt32(3);
                            product = new Product(productId, productName, productPrice, productQuantity);
                        }
                    }
                }
            }
            return product;
        }

        public Product FindProductByName(string name)
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
                            SqlMoney productPrice = reader.GetSqlMoney(2);
                            int productQuantity = reader.GetInt32(3);
                            product = new Product(productId, productName, productPrice, productQuantity);
                        }
                    }
                }
            }
            return product;
        }

        public List<Product> GetAllProducts()
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
                            SqlMoney productPrice = reader.GetSqlMoney(2);
                            int productQuantity = reader.GetInt32(3);
                            Product product = new Product(productId, productName, productPrice, productQuantity);
                            productList.Add(product);
                        }
                    }
                }
            }
            return productList;
        }

        public bool ProductExistById(int id)
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
            return false;

        }

        public bool ProductExistByName(string name)
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
            return false;
        }

        public bool UpdateProduct(int id, Product product)
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
    }
}
