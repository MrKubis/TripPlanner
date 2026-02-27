using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backend.Domain.Entities;

public class Destination
{
    [BsonId]
    [BsonElement("_id"),BsonRepresentation(BsonType.ObjectId)]
    public string? Id {get; set;} = ObjectId.GenerateNewId().ToString();

    [BsonElement("name")]
    public string Name { get; set; }
    [BsonElement("location")]
    public Location Location { get; set; }
    [BsonElement("links")]
    public List<Link> Links { get; set; }
}