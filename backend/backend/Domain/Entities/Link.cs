using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backend.Domain.Entities;

public class Link
{
    [BsonId]
    [BsonElement("_id"),BsonRepresentation(BsonType.ObjectId)]
    public string? Id {get; set;} = ObjectId.GenerateNewId().ToString();
    [BsonElement("url")]
    public string Url { get; set; }
    [BsonElement("title")]
    public string Title { get; set; }
}