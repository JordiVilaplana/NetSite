using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace NetSite.Models;

public record StaticContent
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; init; }

    public string? Path { get; init; }

    public string? Title { get; init; }

    public string? Content { get; init; }
}
