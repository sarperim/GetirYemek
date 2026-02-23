using MongoDB.Driver;

namespace Basket.Infra;

public static class BasketIndexes
{
    public static async Task CreateIndexesAsync(IMongoDatabase database)
    {
        var collection = database.GetCollection<Domain.Basket>("baskets");

        var indexKeys = Builders<Domain.Basket>
            .IndexKeys
            .Ascending(b => b.UserId);

        var indexModel = new CreateIndexModel<Domain.Basket>(
            indexKeys,
            new CreateIndexOptions { Unique = true });

        await collection.Indexes.CreateOneAsync(indexModel);
    }
}
