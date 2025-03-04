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
            // Approach 1: Using FindAsync, which is optimized for primary key lookups.
            // It's simple and fast, returning the entity with the matching ID or null if not found.
            // This approach works well when you're looking up data by the primary key (e.g., Guid).

            return await context.Users.FindAsync(id) ?? null!;  // If the user is not found, returns null.
        }


        public async Task<User> GetById2(Guid id)
        {
            // Approach 2: Using SingleOrDefaultAsync with a WHERE clause.
            // This is more flexible and allows you to add complex filters or joins in the future.
            // It returns a single matching result or null if no match is found.

            // SingleOrDefaultAsync ensures that only **one** or **zero** entity should be returned.
            // If more than one entity is returned, it will throw an exception, which helps catch unexpected data issues (duplicates).
            // This is useful when you're expecting exactly one match or none, ensuring data integrity.

            return await context.Users
                .Where(u => u.Id == id)   // Applying a filter to match by ID.
                .SingleOrDefaultAsync() ?? null!;   // Will throw if multiple users are found with the same ID, enforcing uniqueness.
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
