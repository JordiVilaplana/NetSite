using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace NetSite.Models;

public record StaticPage
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; init; }

    public string? Route { get; init; }
    
    public string? Title { get; init; }
    
    public string? Content { get; init; }
    
    public int? Order { get; init; }
    
    public IList<StaticPage> NestedPages { get; init; } = new List<StaticPage>();
}
