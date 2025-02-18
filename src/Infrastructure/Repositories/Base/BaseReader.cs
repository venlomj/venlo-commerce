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
        public abstract Task<T> GetById(Guid id);
        public abstract Task<IEnumerable<T>> MultipleByValue(IEnumerable<string> values);

        public abstract Task<bool> Exists(Guid id);
    }
}
