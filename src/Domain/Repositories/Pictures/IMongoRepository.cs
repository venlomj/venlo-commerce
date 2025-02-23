using Domain.Attributes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories.Pictures
{
    public interface IMongoRepository<TDocument> where TDocument : IDocument
    {
        Task<IEnumerable<TDocument>> GetAll();
        IEnumerable<TDocument> FilterBy(Expression<Func<TDocument, bool>> expression);
        Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> expression);

        Task<TDocument> FindByIdAsync(Guid id);

        TDocument FindById(Guid id);

        Task InsertOneAsync(TDocument document);

        Task InsertManyAsync(ICollection<TDocument> documents);

        Task ReplaceOneAsync(TDocument document);

        void DeleteById(Guid id);

        Task DeleteByIdAsync(Guid id);
    }
}
