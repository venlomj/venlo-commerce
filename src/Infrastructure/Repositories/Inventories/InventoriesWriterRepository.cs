using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories.Inventories;
using Infrastructure.Persistence.SQL;
using Infrastructure.Repositories.Base;

namespace Infrastructure.Repositories.Inventories
{
    public class InventoriesWriterRepository : BaseWriter<StockItem>,
        IInventoriesWriterRepository
    {
        private readonly VenloCommerceDbContext _context;
        public InventoriesWriterRepository(VenloCommerceDbContext context)
        {
            _context = context;
        }
        public override Task<Guid> Add(StockItem entity)
        {
            throw new NotImplementedException();
        }

        public override Task<StockItem> Update(StockItem entity)
        {
            _context.StockItems.Update(entity);
            return Task.FromResult(entity);
        }

        public override void Delete(StockItem entity)
        {
            throw new NotImplementedException();
        }
    }
}
