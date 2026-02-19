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
    [BsonElement("destinations")]
    public List<Destination> Destinations { get; set; } = new List<Destination>();
}