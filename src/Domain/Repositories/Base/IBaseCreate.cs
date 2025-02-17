using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories.Base
{
    public interface IBaseCreate<T>
        where T : class, new()
    {
        Task<Guid> Add(T entity);
    }
}
