using MongoDB.Bson.Serialization.Attributes;

namespace backend.Domain.Entities;

public class Location
{
    [BsonElement("latitude")]
    public double Latitude {get; set;}
    [BsonElement("longitude")]
    public double Longitude {get; set;}
}