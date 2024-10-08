using MongoDB.Driver;
using NetSite.Models;

namespace NetSite.Services;

public class NavItemsService
{
    private readonly IMongoCollection<NavItem> _navItemsCollection;

    public NavItemsService(MongoDbService dbService)
    {
        _navItemsCollection = dbService.GetNavItemsCollection();
    }

    public async Task<IEnumerable<string>> ListTagsAsync()
    {
        var tags = await _navItemsCollection.Find(_ => true).ToListAsync();
        return tags.Select(i => i.Tag).Distinct().Order();
    }

    public async Task<IEnumerable<NavItem>> ListAsync(string tag)
    {
        var items = await _navItemsCollection
            .Find(i => i.Tag == tag).ToListAsync();
        return items.OrderBy(n => n.Order);
    }

    public async Task<NavItem> GetAsync(string id)
    {
        return await _navItemsCollection.Find(p => p.Id == id).FirstOrDefaultAsync();
    }

    public async Task CreateAsync(NavItem value)
    {
        await _navItemsCollection.InsertOneAsync(value);
    }

    public async Task UpdateAsync(NavItem value)
    {
        await _navItemsCollection.ReplaceOneAsync(p => p.Id == value.Id, value);
    }

    public async Task DeleteAsync(NavItem value)
    {
        await _navItemsCollection.DeleteOneAsync(p => p.Id == value.Id);
    }
}
