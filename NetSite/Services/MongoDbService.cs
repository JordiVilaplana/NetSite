using Microsoft.Extensions.Options;
using MongoDB.Driver;
using NetSite.Models;
using NetSite.Settings;

namespace NetSite.Services;

public class MongoDbService
{
    private readonly IMongoDatabase _database;

    public MongoDbService(
        IOptions<MongoDbSettings> settings,
        IMongoClient client)
    {
        var dbName = settings?.Value?.DatabaseName ??
            throw new ArgumentNullException(nameof(settings));

        _database = client.GetDatabase(dbName);
    }

    public IMongoCollection<StaticContent> GetStaticContentCollection()
    {
        return _database.GetCollection<StaticContent>("StaticContent");
    }

    public async Task FlushDatabaseAsync()
    {
        var collections = await _database.ListCollectionNamesAsync();
        while (collections.MoveNext())
        {
            foreach (var collection in collections.Current)
            {
                await _database.DropCollectionAsync(collection);
            }
        }
    }

    public async Task SeedDatabaseAsync()
    {
        var scCollection = GetStaticContentCollection();
        var scCount = await scCollection.CountDocumentsAsync(_ => true);
        if (scCount == 0)
        {
            var initialStaticContent = new List<StaticContent>
            {
                new StaticContent()
                {
                    Path = "/index",
                    Name = "Home",
                    Title = "Home",
                    Content = "Hello, World!",
                    IsTopLevel = true,
                }
            };
            await scCollection.InsertManyAsync(initialStaticContent);
        }
    }
}
