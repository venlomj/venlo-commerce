using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories.Base
{
    public interface IBaseDelete<in T> where T : class, new()
    {
        //Task DeleteById(Guid id);
        void Delete(T entity);
    }
}
