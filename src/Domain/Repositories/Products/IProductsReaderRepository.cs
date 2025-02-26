using Domain.Entities;
using Domain.Repositories.Base;

namespace Domain.Repositories.Products
{
    public interface IProductsReaderRepository : IBaseRead<Product>,
        IBaseVerify<Product>
    {
        Task<IEnumerable<Product>> GetByCategoryIdAsync(Guid categoryId);
    }
}
