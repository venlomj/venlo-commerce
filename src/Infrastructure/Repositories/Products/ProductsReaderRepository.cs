using Domain.Entities;
using Domain.Repositories.Products;
using Infrastructure.Persistence.SQL;
using Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Products
{
    public class ProductsReaderRepository : BaseReader<Product>,
        IProductsReaderRepository
    {
        private readonly VenloCommerceDbContext _context;
        public ProductsReaderRepository(VenloCommerceDbContext context)
        {
            _context = context;
        }
        public override async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Products.ToListAsync();

        }

        public override async Task<Product> GetById(Guid id)
        {
            return await _context.Products.FindAsync(id) ?? null!;
        }

        public override async Task<IEnumerable<Product>> MultipleByValue(IEnumerable<string> values)
        {
            return await _context.Products
                .Where(p => values.Contains(p.SkuCode))
                .ToListAsync();
        }

        public override async Task<bool> Exists(Guid id)
        {
            return await _context.Products.AnyAsync(x => x.Id == id);
        }
    }
}
