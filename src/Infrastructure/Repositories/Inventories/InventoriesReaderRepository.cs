using Domain.Entities;
using Domain.Repositories.Inventories;
using Infrastructure.Persistence;
using Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Inventories
{
    public class InventoriesReaderRepository : BaseReader<StockItem>,
        IInventoriesReaderRepository
    {
        private readonly VenloCommerceDbContext _context;
        public InventoriesReaderRepository(VenloCommerceDbContext context)
        {
            _context = context;
        }
        public override Task<IEnumerable<StockItem>> GetAll()
        {
            throw new NotImplementedException();
        }

        public override Task<StockItem> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public override async Task<IEnumerable<StockItem>> MultipleByValue(IEnumerable<string> values)
        {
            return await _context.StockItems
                .Where(i => values.Contains(i.Product.SkuCode))
                .ToListAsync();
        }

        public override Task<bool> Exists(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
