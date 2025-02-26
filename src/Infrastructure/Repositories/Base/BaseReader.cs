using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Repositories.Base;

namespace Infrastructure.Repositories.Base
{
    public abstract class BaseReader<T> : IBaseRead<T>, IBaseVerify<T>
       where T : class, new()
    {
        public abstract Task<IEnumerable<T>> GetAll();

        public abstract Task<IEnumerable<T>> GetFiltered(Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, int page = 1, int pageSize = 10);

        public abstract Task<T> GetById(Guid id);
        public abstract Task<IEnumerable<T>> MultipleByValue(IEnumerable<string> values);
        public abstract Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null);

        public abstract Task<List<T>> GetPagedAsync(int page, int pageSize, Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null);

        public abstract Task<bool> Exists(Guid id);
    }
}
