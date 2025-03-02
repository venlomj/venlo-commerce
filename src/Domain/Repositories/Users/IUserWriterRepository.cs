using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories.Base;

namespace Domain.Repositories.Users
{
    public interface IUserWriterRepository : IBaseCreate<User>,
        IBaseUpdate<User>, IBaseDelete<User>
    {
    }
}
