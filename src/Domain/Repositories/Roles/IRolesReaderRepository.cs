using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories.Base;

namespace Domain.Repositories.Roles
{
    public interface IRolesReaderRepository : IBaseRead<Role, Guid>
    {
        Task<List<Role>> GetRolesByUserId(Guid userId);
    }
}
