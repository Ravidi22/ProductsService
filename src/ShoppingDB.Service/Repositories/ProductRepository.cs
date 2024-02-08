using MongoDB.Driver;
using ShoppingDB.Service.Entities;

namespace ShoppingDB.Service.Repositories
{
    public class ProductRepository
    {
        private const string collectionName = "Products";
        private readonly IMongoCollection<Product> dbCollection;
        private readonly FilterDefinitionBuilder<Product> filterBuilder = Builders<Product>.Filter;

        public ProductRepository()
        {
            var mongoClient = new MongoClient("mongodb://localhost:27017");

            var dataBase = mongoClient.GetDatabase("ShoppingCart");

            dbCollection = dataBase.GetCollection<Product>(collectionName);
        }

        public async Task<IReadOnlyCollection<Product>> GetAllAsync()
        {
            return await dbCollection.Find(filterBuilder.Empty).ToListAsync();
        }

        public async Task<Product> GetAsync(Guid id)
        {
            FilterDefinition<Product> filter = filterBuilder.Eq(entity => entity.Id, id);
            return await dbCollection.Find(filterBuilder.Empty).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Product entity)
        {
            if (entity == null)
            {
                throw new ArgumentException(nameof(entity));
            }
            await dbCollection.InsertOneAsync(entity);
        }

        public async Task CreateBulkAsync(List<Product> entitys)
        {
            foreach (var product in entitys)
            {
                await dbCollection.InsertOneAsync(product);
            }
        }

        public async Task UpdateAsync(Product entity)
        {        
            if (entity == null)
            {
                throw new ArgumentException(nameof(entity));
            }

            FilterDefinition<Product> filter = filterBuilder.Eq(existingEntity => existingEntity.Id, entity.Id);
            await dbCollection.ReplaceOneAsync(filter, entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            FilterDefinition<Product> filter = filterBuilder.Eq(entity => entity.Id, id);

            await dbCollection.DeleteOneAsync(filter);
        }
    }
}
