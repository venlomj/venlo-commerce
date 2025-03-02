using Domain.Entities;
using Domain.Repositories.Base;

namespace Domain.Repositories.Products
{
    public interface IProductsReaderRepository : IBaseRead<Product, Guid>,
        IBaseVerify<Product, Guid>
    {
        Task<IEnumerable<Product>> GetByCategoryIdAsync(int categoryId);
    }
}
