using MongoDB.Bson.Serialization.Attributes;

namespace backend.Domain.Entities;

public class Expense
{
    [BsonElement("name")]
    public string Name { get; set; }
    [BsonElement("description")]
    public string Description { get; set; }
    [BsonElement("price")]
    public double Price {get; set;}
}