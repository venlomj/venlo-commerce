using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Repositories.Base;

namespace Infrastructure.Repositories.Base
{
    public abstract class BaseWriter<T> : IBaseCreate<T>,
        IBaseUpdate<T>, IBaseDelete<T> where T : class, new()
    {
        public abstract Task<Guid> Add(T entity);
        public abstract Task<T> Update(T entity);

        //public abstract Task DeleteById(Guid id);
        public abstract void Delete(T entity);
    }
}
