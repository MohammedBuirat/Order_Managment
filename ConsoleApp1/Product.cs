using System.Data.SqlTypes;
using System.Text.Json.Serialization;

namespace ConsoleApp1
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public Product(int id, string name, decimal price, int quantity)
        {
            Id = id;
            Name = name;
            Price = price;
            Quantity = quantity;
        }

        public Product() { }

        public override string ToString()
        {
            return $"Product ID:  {Id}  Product Name:  {Name}  Price:    {Price}  Quantity: {Quantity}";
        }
    }
}
