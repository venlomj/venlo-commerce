using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories.Users;
using Infrastructure.Persistence.SQL;
using Infrastructure.Repositories.Base;

namespace Infrastructure.Repositories.Users
{
    public class UserWriterRepository(VenloCommerceDbContext context) : BaseWriter<User>,
        IUserWriterRepository
    {
        //private readonly VenloCommerceDbContext _context = context;

        public override async Task<Guid> Add(User entity)
        {
            await context.Users.AddAsync(entity);
            return entity.Id;
        }

        public override Task<User> Update(User entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
