using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace NetSite.Models;

public record NavItem
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; init; }

    public required string Path { get; init; }

    public required string Name { get; init; }

    public NavTag[] Tags { get; init; } = Array.Empty<NavTag>();
}
