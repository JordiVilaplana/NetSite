using MongoDB.Driver;

namespace NetSite.Services;

public class MongoDbService
{
    private readonly IMongoClient _client;

    public MongoDbService(IMongoClient client)
    {
        _client = client;
    }
}
