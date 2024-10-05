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

    public IMongoCollection<StaticPage> GetPagesCollection()
    {
        return _database.GetCollection<StaticPage>("StaticPages");
    }
}
