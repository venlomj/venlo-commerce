using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories.Products;
using Infrastructure.Persistence;
using Infrastructure.Repositories.Base;

namespace Infrastructure.Repositories.Products
{
    public class ProductWriterRepository : BaseWriter<Product>,
        IProductWriterRepository
    {
        private readonly VenloCommerceDbContext _context;
        public ProductWriterRepository(VenloCommerceDbContext context)
        {
            _context = context;
        }
        public override async Task<Guid> Add(Product entity)
        {
            await _context.Products.AddAsync(entity);
            return entity.Id;
        }

        public override Task<Product> Update(Product entity)
        {
            _context.Products.Update(entity); // Track changes
            return Task.FromResult(entity); // Return the entity without saving changes here
        }

        public override void Delete(Product entity)
        {
            _context.Products.Remove(entity);
        }
    }
}
