using MongoDB.Driver;
using NetSite.Models;

namespace NetSite.Services;

public class PagesService
{
    private static readonly StaticPage DefaultPage = new StaticPage()
    {
        Id = string.Empty,
        Route = "/index",
        Title = "Home",
        Content = "Hello, World!"
    };

    private readonly IMongoCollection<StaticPage> _pagesCollection;

    public PagesService(MongoDbService dbService)
    {
        _pagesCollection = dbService.GetPagesCollection();
    }

    public async Task<IEnumerable<StaticPage>> ListAsync()
    {
        if (await _pagesCollection.CountDocumentsAsync(_ => true) == 0)
            await CreateAsync(DefaultPage);
        return await _pagesCollection.Find(_ => true).ToListAsync();
    }

    public async Task<StaticPage> GetAsync(string id)
    {
        return await _pagesCollection.Find(p => p.Id == id).FirstOrDefaultAsync();
    }

    public async Task CreateAsync(StaticPage value)
    {
        await _pagesCollection.InsertOneAsync(value);
    }

    public async Task UpdateAsync(string id, StaticPage value)
    {
        await _pagesCollection.ReplaceOneAsync(p => p.Id == id, value);
    }

    public async Task DeleteAsync(string id)
    {
        await _pagesCollection.DeleteOneAsync(p => p.Id == id);
    }
}
