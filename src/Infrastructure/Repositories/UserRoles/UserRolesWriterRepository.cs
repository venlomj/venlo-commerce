using Domain.Entities;
using Domain.Repositories.UserRoles;
using Infrastructure.Persistence.SQL;
using Infrastructure.Repositories.Base;

namespace Infrastructure.Repositories.UserRoles
{
    public class UserRolesWriterRepository : BaseWriter<UserRole>,
        IUserRolesWriterRepository
    {
        private readonly VenloCommerceDbContext _context;
        public UserRolesWriterRepository(VenloCommerceDbContext context)
        {
            _context = context;
        }
        public override async Task<Guid> Add(UserRole entity)
        {
            await _context.UserRoles.AddAsync(entity);
            return entity.Id;
        }

        public override Task<UserRole> Update(UserRole entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(UserRole entity)
        {
            throw new NotImplementedException();
        }
    }
}
