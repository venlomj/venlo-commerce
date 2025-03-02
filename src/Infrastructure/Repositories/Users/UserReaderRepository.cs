using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories.Users;
using Infrastructure.Persistence.SQL;
using Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Users
{
    public class UserReaderRepository(VenloCommerceDbContext context) : BaseReader<User, Guid>,
        IUserReaderRepository
    {
        public override Task<IEnumerable<User>> GetAll()
        {
            throw new NotImplementedException();
        }

        public override Task<IEnumerable<User>> GetFiltered(Expression<Func<User, bool>>? filter = null, Func<IQueryable<User>, IOrderedQueryable<User>>? orderBy = null, int page = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        public override async Task<User> GetById(Guid id)
        {
            return await context.Users.FindAsync(id) ?? null!;
        }

        public override Task<IEnumerable<User>> MultipleByValue(IEnumerable<string> values)
        {
            throw new NotImplementedException();
        }

        public override Task<int> CountAsync(Expression<Func<User, bool>>? predicate = null)
        {
            throw new NotImplementedException();
        }

        public override Task<List<User>> GetPagedAsync(int page, int pageSize, Expression<Func<User, bool>>? filter = null, Func<IQueryable<User>, IOrderedQueryable<User>>? orderBy = null)
        {
            throw new NotImplementedException();
        }

        public override async Task<bool> Exists(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UserExists(string email)
        {
            return await context.Users.AnyAsync(x => x.Email == email);
        }

        public async Task<User?> ByEmail(string email)
        {
            return await context.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Email == email);

        }
    }
}
