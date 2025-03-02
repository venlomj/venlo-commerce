using Domain.Entities;
using Domain.Repositories.Base;

namespace Domain.Repositories.Users
{
    public interface IUserReaderRepository : IBaseRead<User, Guid>,
        IBaseVerify<User, Guid>
    {
        Task<bool> UserExists(string email);
        Task<User> ByEmail(string email);
    }
}
