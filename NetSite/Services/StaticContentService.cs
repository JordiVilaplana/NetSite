using MongoDB.Driver;
using NetSite.Models;

namespace NetSite.Services;

public class StaticContentService
{
    private readonly IMongoCollection<StaticContent> _pagesCollection;

    public StaticContentService(MongoDbService dbService)
    {
        _pagesCollection = dbService.GetStaticContentCollection();
    }

    public async Task<IEnumerable<StaticContent>> ListAsync()
    {
        return await _pagesCollection.Find(_ => true).ToListAsync();
    }

    public async Task<StaticContent> GetAsync(string id)
    {
        return await _pagesCollection.Find(p => p.Id == id).FirstOrDefaultAsync();
    }

    public async Task<StaticContent> GetByPathAsync(string path)
    {
        return await _pagesCollection.Find(p => p.Path == path).FirstOrDefaultAsync();
    }

    public async Task CreateAsync(StaticContent value)
    {
        await _pagesCollection.InsertOneAsync(value);
    }

    public async Task UpdateAsync(string id, StaticContent value)
    {
        await _pagesCollection.ReplaceOneAsync(p => p.Id == id, value);
    }

    public async Task DeleteAsync(string id)
    {
        await _pagesCollection.DeleteOneAsync(p => p.Id == id);
    }
}
