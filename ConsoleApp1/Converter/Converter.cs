using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Coverter
{
    internal class Converter
    {
        public BsonProduct ProductToBson(Product product)
        {
            BsonProduct bsonProduct = new BsonProduct();
            bsonProduct.Price = product.Price;
            bsonProduct.Quantity = product.Quantity;
            bsonProduct.Name = product.Name;
            bsonProduct.Id = product.Id;
            return bsonProduct;
        }

        public Product BsonToProduct(BsonProduct bsonProduct)
        {
            Product product = new Product();
            product.Price = bsonProduct.Price;
            product.Quantity = bsonProduct.Quantity;
            product.Name = bsonProduct.Name;
            product.Id = bsonProduct.Id;
            return product;
        }
    }
}
