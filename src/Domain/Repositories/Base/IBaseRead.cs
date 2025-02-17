using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories.Base
{
    public interface IBaseRead<T>
      where T : class, new()
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(Guid id);
    }
}
