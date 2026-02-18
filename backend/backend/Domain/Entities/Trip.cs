using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backend.Domain.Entities;

public class Trip
{
    [BsonId]
    [BsonElement("_id"),BsonRepresentation(BsonType.ObjectId)]
    public string? Id {get; set;}
    
    [BsonElement("title")] 
    public string? Title;
    
    [BsonElement("created_by"),BsonRepresentation(BsonType.ObjectId)]
    public string? CreatedBy {get; set;}
    
    [BsonElement("created_on")]
    public DateTime CreatedOn {get; set;}
    
    [BsonElement("destinations")]
    public List<Destination> Destinations {get; set;}
    
    [BsonElement("links")]
    public List<Link> Links {get; set;}
    
    [BsonElement("days")]
    public List<Day> Days {get; set;}
    
    [BsonElement("expenses")]
    public List<Expense> Expenses {get; set;}

}