using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories.Orders;
using Infrastructure.Persistence.SQL;
using Infrastructure.Repositories.Base;

namespace Infrastructure.Repositories.Orders
{
    public class OrderWriterRepository(VenloCommerceDbContext context) : BaseWriter<Order>,
        IOrderWriterRepository
    {
        public override async Task<Guid> Add(Order entity)
        {
            await context.Orders.AddAsync(entity);
            return entity.Id;
        }

        public override Task<Order> Update(Order entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Order entity)
        {
            throw new NotImplementedException();
        }
    }
}
