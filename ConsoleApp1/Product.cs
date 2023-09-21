using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_Managment
{
    internal class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public SqlMoney Price { get; set; }
        public int Quantity { get; set; }

        public Product(int id, string name, SqlMoney price, int quantity)
        {
            this.Id = id;
            this.Name = name;
            this.Price = price;
            this.Quantity = quantity;
        }

        public Product() { }

        public override string ToString()
        {
            return $"Product ID:  {this.Id}  Product Name:  {this.Name}  Price:    {this.Price}  Quantity: {this.Quantity}";
        }
    }
}
