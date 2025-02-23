using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories.Orders;
using Infrastructure.Persistence.SQL;
using Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Orders
{
    public class OrdersReaderRepository : BaseReader<Order>,
        IOrdersReaderRepository
    {
        private readonly VenloCommerceDbContext _context;
        public OrdersReaderRepository(VenloCommerceDbContext context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<Order>> GetAll()
        {
           return await _context.Orders
                .Include(x => x.OrderLineItems)
                .ThenInclude(ol => ol.Product)
                .ToListAsync();
        }

        public override Task<Order> GetById(Guid id)
        {
            return _context.Orders
                .Include(ol => ol.OrderLineItems)
                .ThenInclude(p => p.Product)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public override Task<IEnumerable<Order>> MultipleByValue(IEnumerable<string> values)
        {
            throw new NotImplementedException();
        }

        public override Task<bool> Exists(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
