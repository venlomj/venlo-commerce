using Domain.Entities;
using Domain.Repositories.Base;

namespace Domain.Repositories.Inventories
{
    public interface IInventoriesReaderRepository : IBaseRead<StockItem, Guid>
    {
        Task<bool> InStock(Guid productId);
        Task<StockItem?> GetByProductIdAsync(Guid productId);
    }
}
