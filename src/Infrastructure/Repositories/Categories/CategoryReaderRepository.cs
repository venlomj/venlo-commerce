using System.Linq.Expressions;
using Domain.Entities;
using Domain.Repositories.Categories;
using Infrastructure.Persistence.SQL;
using Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Categories
{
    public class CategoryReaderRepository(VenloCommerceDbContext context) : BaseReader<Category, int>,
        ICategoryReaderRepository
    {
        public override Task<IEnumerable<Category>> GetAll()
        {
            throw new NotImplementedException();
        }

        public override Task<IEnumerable<Category>> GetFiltered(Expression<Func<Category, bool>>? filter = null, Func<IQueryable<Category>, IOrderedQueryable<Category>>? orderBy = null, int page = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        public override async Task<Category> GetById(int id)
        {
            return await context.Categories.FindAsync(id) ?? null!;
        }

        public override Task<IEnumerable<Category>> MultipleByValue(IEnumerable<string> values)
        {
            throw new NotImplementedException();
        }

        public override Task<int> CountAsync(Expression<Func<Category, bool>>? predicate = null)
        {
            throw new NotImplementedException();
        }

        public override Task<List<Category>> GetPagedAsync(int page, int pageSize, Expression<Func<Category, bool>>? filter = null, Func<IQueryable<Category>, IOrderedQueryable<Category>>? orderBy = null)
        {
            throw new NotImplementedException();
        }

        public override async Task<bool> Exists(int id)
        {
            return await context.Categories.AnyAsync(x => x.Id == id);
        }
    }
}
