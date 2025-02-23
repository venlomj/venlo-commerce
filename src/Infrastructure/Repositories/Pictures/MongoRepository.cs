
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Attributes;
using Domain.Attributes.Interfaces;
using Domain.Documents;
using Domain.Repositories.Pictures;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace Infrastructure.Repositories.Pictures
{
    public class MongoRepository<TDocument> : IMongoRepository<TDocument> where TDocument : IDocument
    {
        private readonly IMongoCollection<TDocument> _collection;
        private readonly IMongoCollection<ProductImage> _imageCollection;

        public MongoRepository(IMongoDbSettings settings)
        {
            if (settings == null || string.IsNullOrEmpty(settings.ConnectionString))
            {
                throw new ArgumentException("MongoDB connection string is null or empty.");
            }

            var database = new MongoClient(settings.ConnectionString).GetDatabase(settings.DatabaseName);
            _collection = database.GetCollection<TDocument>(GetCollectionName(typeof(TDocument)));
        }

        public async Task<IEnumerable<TDocument>> GetAll()
        {
            return await _collection.AsQueryable().ToListAsync();
        }

        public virtual TDocument FindById(Guid id)
        {
            var filter = Builders<TDocument>.Filter.Eq(x => x.Id, id);
            return _collection.Find(filter).SingleOrDefault();
        }

        public virtual IEnumerable<TDocument> FilterBy(Expression<Func<TDocument, bool>> expression)
        {
            return _collection.Find(expression).ToEnumerable();
        }

        public virtual async Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> expression)
        {
            return await _collection.Find(expression).FirstOrDefaultAsync();
        }

        public virtual async Task<TDocument> FindByIdAsync(Guid id)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, id);
            return await _collection.Find(filter).SingleOrDefaultAsync();
        }

        public virtual async Task InsertOneAsync(TDocument document)
        {
            await _collection.InsertOneAsync(document);
        }

        public virtual async Task InsertManyAsync(ICollection<TDocument> documents)
        {
            await _collection.InsertManyAsync(documents);
        }

        public virtual async Task ReplaceOneAsync(TDocument document)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, document.Id);
            await _collection.FindOneAndReplaceAsync(filter, document);
        }

        public void DeleteById(Guid id)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, id);
            _collection.FindOneAndDelete(filter);
        }

        public Task DeleteByIdAsync(Guid id)
        {
            return Task.Run(() =>
            {
                var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, id);
                _collection.FindOneAndDeleteAsync(filter);
            });
        }

        private protected string? GetCollectionName(Type documentType)
        {
            return ((BsonCollectionAttribute)documentType.GetCustomAttributes(
                    typeof(BsonCollectionAttribute),
                    true)
                .FirstOrDefault())?.CollectionName;
        }
    }
}
