using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories.Base
{
    public interface IBaseVerify<T>
        where T : class, new()
    {
        public Task<bool> Exists(Guid id);
    }
}
