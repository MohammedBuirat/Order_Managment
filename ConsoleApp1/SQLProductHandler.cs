using Microsoft.Data.SqlClient;
using System.Text;

namespace Order_Managment
{
    internal class SQLProductHandler : IProductHandler
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

        public bool EditProduct(int id, Product newProduct)
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
                    command.Parameters.AddWithValue("@New_Name", newProduct.Name);
                    command.Parameters.AddWithValue("@New_Price", newProduct.Price);
                    command.Parameters.AddWithValue("@New_Quantity", newProduct.Quantity); // Corrected parameter name

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
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

        public Product GetProductById(int id)
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

        public Product GetProductByName(string name)
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
    }
}
