using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories.Roles;
using Infrastructure.Persistence.SQL;
using Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Roles
{
    public class RolesReaderRepository : BaseReader<Role, Guid>,
        IRolesReaderRepository
    {
        private readonly VenloCommerceDbContext _context;

        public RolesReaderRepository(VenloCommerceDbContext context)
        {
            _context = context;
        }

        public override Task<IEnumerable<Role>> GetAll()
        {
            throw new NotImplementedException();
        }

        public override Task<IEnumerable<Role>> GetFiltered(Expression<Func<Role, bool>>? filter = null, Func<IQueryable<Role>, IOrderedQueryable<Role>>? orderBy = null, int page = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        public override async Task<Role> GetById(Guid id)
        {
            return await _context.Roles.FindAsync(id) ?? null!;
        }

        public override Task<IEnumerable<Role>> MultipleByValue(IEnumerable<string> values)
        {
            throw new NotImplementedException();
        }

        public override Task<int> CountAsync(Expression<Func<Role, bool>>? predicate = null)
        {
            throw new NotImplementedException();
        }

        public override Task<List<Role>> GetPagedAsync(int page, int pageSize, Expression<Func<Role, bool>>? filter = null, Func<IQueryable<Role>, IOrderedQueryable<Role>>? orderBy = null)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Role>> GetRolesByUserId(Guid userId)
        {
            return await _context.UserRoles
                .Where(ur => ur.UserId == userId)
                .Select(ur => ur.Role)
                .ToListAsync();
        }

        public override Task<bool> Exists(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
