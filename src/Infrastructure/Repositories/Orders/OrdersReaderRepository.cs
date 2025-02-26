using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories.Orders;
using Infrastructure.Persistence.SQL;
using Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Orders
{
    public class OrdersReaderRepository(VenloCommerceDbContext context) : BaseReader<Order>,
        IOrdersReaderRepository
    {
        public override async Task<IEnumerable<Order>> GetAll()
        {
           return await context.Orders
                .Include(x => x.OrderLineItems)
                .ThenInclude(ol => ol.Product)
                .ToListAsync();
        }

        public override async Task<IEnumerable<Order>> GetFiltered(
            Expression<Func<Order, bool>>? filter = null,
            Func<IQueryable<Order>, IOrderedQueryable<Order>>? orderBy = null,
            int page = 1,
            int pageSize = 10)
        {
            IQueryable<Order> query = context.Orders.Include(o => o.OrderLineItems)
                .ThenInclude(oli => oli.Product);

            if (filter != null)
                query = query.Where(filter);

            query = orderBy != null ? orderBy(query) : query.OrderBy(o => o.DateCreated); 

            return await query.Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }



        public override Task<Order> GetById(Guid id)
        {
            return context.Orders
                .Include(ol => ol.OrderLineItems)
                .ThenInclude(p => p.Product)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public override Task<IEnumerable<Order>> MultipleByValue(IEnumerable<string> values)
        {
            throw new NotImplementedException();
        }

        public override async Task<int> CountAsync(Expression<Func<Order, bool>>? predicate = null)
        {
            return predicate == null ?
                await context.Orders.CountAsync()
                : await context.Orders.CountAsync(predicate);
        }

        public override async Task<List<Order>> GetPagedAsync(int page, int pageSize, Expression<Func<Order, bool>>? filter = null, Func<IQueryable<Order>, IOrderedQueryable<Order>>? orderBy = null)
        {
            IQueryable<Order> query = context.Orders;

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = orderBy(query);

            return await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public override async Task<bool> Exists(Guid id)
        {
            return await context.Orders.AnyAsync(x => x.Id == id);
        }
    }
}
