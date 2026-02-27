using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backend.Domain.Entities;

public class Destination
{
    [BsonId]
    [BsonElement("_id"),BsonRepresentation(BsonType.ObjectId)]
    public string? Id {get; set;} = ObjectId.GenerateNewId().ToString();

    public string Name { get; set; }
    public Location Location { get; set; }
    public List<Link> Links { get; set; }
}