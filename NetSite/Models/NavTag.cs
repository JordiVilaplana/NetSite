namespace NetSite.Models;

public record NavTag
{
    public required string Name { get; init; }
    public required int Order { get; init; }
}
