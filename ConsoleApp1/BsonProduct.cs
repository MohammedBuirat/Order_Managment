using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class BsonProduct
    {
        [BsonId, BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string ProductId { get; set; }
        [BsonElement("Id"), BsonRepresentation(BsonType.Int32)]
        public int Id { get; set; }
        [BsonElement("Name"), BsonRepresentation(BsonType.String)]
        public string Name { get; set; }
        [BsonElement("Price"), BsonRepresentation(BsonType.Decimal128)]

        public decimal Price { get; set; }
        [BsonElement("Quantity"), BsonRepresentation(BsonType.Int32)]
        public int Quantity { get; set; }
    }
}
