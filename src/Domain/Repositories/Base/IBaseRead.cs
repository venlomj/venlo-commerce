using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories.Base
{
    public interface IBaseRead<T, in TKey>  // Add TKey here
        where T : class, new()
    {
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetFiltered(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            int page = 1,
            int pageSize = 10
        );
        Task<T> GetById(TKey id);
        Task<IEnumerable<T>> MultipleByValue(IEnumerable<string> values);
        Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null);
        Task<List<T>> GetPagedAsync(int page, int pageSize, Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null);
    }
}
