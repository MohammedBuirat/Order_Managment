using ConsoleApp1.Coverter;
using MongoDB.Driver;
using System.Xml.Linq;

namespace ConsoleApp1.DataAccessLayer.MongoAccess
{
    internal class MongoProductAccess : IProductDataAccess
    {
        private readonly IMongoCollection<BsonProduct> productCollection;

        public MongoProductAccess()
        {
            string connectionString = "mongodb://localhost:27017";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Order_Managment");
            productCollection = database.GetCollection<BsonProduct>("Products");
        }
        public bool AddProduct(Product product)
        {
            try
            {
                BsonProduct bsonProduct = new BsonProduct();
                bsonProduct.Name = product.Name;
                bsonProduct.Id = GetMaxProductId() + 1;
                bsonProduct.Price = product.Price;
                bsonProduct.Quantity = product.Quantity;
                productCollection.InsertOne(bsonProduct);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteProduct(int id)
        {
            try
            {
                var filter = Builders<BsonProduct>.Filter.Eq(a => a.Id, id);
                productCollection.DeleteOne(filter);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Product FindProductById(int id)
        {
            var filter = Builders<BsonProduct>.Filter.Eq(a => a.Id, id);
            var bsonProduct = productCollection.Find(filter).FirstOrDefault();
            return new Converter().BsonToProduct(bsonProduct);
        }

        public Product FindProductByName(string name)
        {
            var filter = Builders<BsonProduct>.Filter.Eq(a => a.Name, name);
            var bsonProduct = productCollection.Find(filter).FirstOrDefault();
            return new Converter().BsonToProduct(bsonProduct);
        }

        public List<Product> GetAllProducts()
        {
            var filter = Builders<BsonProduct>.Filter.Empty;
            var bsonProducts = productCollection.Find(filter).ToList<BsonProduct>();
            List<Product> products = new List<Product>();
            foreach (var bson in bsonProducts)
            {
                products.Add(new Converter().BsonToProduct(bson));
            }
            return products;
        }

        public bool ProductExistById(int id)
        {
            var filter = Builders<BsonProduct>.Filter.Eq(a => a.Id, id);
            var bsonProduct = productCollection.Find(filter).FirstOrDefault();
            return bsonProduct != null;
        }

        public bool ProductExistByName(string name)
        {
            var filter = Builders<BsonProduct>.Filter.Eq(a => a.Name, name);
            var bsonProduct = productCollection.Find(filter).FirstOrDefault();
            return bsonProduct != null;
        }

        public bool UpdateProduct(int id, Product product)
        {
            try
            {
                var filter = Builders<BsonProduct>.Filter.Eq(a => a.Id, id);
                var updateDefinition = Builders<BsonProduct>.Update
                .Set(a => a.Name, product.Name)
                .Set(a => a.Quantity, product.Quantity)
                .Set(a => a.Price, product.Price);
                productCollection.UpdateOne(filter, updateDefinition);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public int GetMaxProductId()
        {
            var sortDefinition = Builders<BsonProduct>.Sort.Descending(a => a.Id);
            var result = productCollection.Find(Builders<BsonProduct>.Filter.Empty)
                                          .Sort(sortDefinition)
                                          .Limit(1)
                                          .Project<BsonProduct>(Builders<BsonProduct>.Projection.Include(a => a.Id))
                                          .FirstOrDefault();
            if (result != null)
            {
                return result.Id;
            }
            return 0;
        }
    }
}