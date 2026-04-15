using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backend.Domain.Entities;

public class Day
{
    [BsonId]
    [BsonElement("_id"),BsonRepresentation(BsonType.ObjectId)]
    public string? Id {get; set;} = ObjectId.GenerateNewId().ToString();
    [BsonElement("date")]
    public DateOnly Date {get; set;}
    [BsonElement("destinationIds")]
    public List<string> DestinationIds { get; set; } = new List<string>();
}