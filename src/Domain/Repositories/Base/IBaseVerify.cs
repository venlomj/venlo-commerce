
namespace Domain.Repositories.Base
{
    public interface IBaseVerify<T, in TKey>
        where T : class, new()
    {
        Task<bool> Exists(TKey id);
    }
}
