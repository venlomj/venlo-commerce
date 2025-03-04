﻿using System.Linq.Expressions;
using Domain.Entities;
using Domain.Repositories.Inventories;
using Infrastructure.Persistence.SQL;
using Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Inventories
{
    public class InventoriesReaderRepository(VenloCommerceDbContext context) : BaseReader<StockItem, Guid>,
        IInventoriesReaderRepository
    {
        public override Task<IEnumerable<StockItem>> GetAll()
        {
            throw new NotImplementedException();
        }

        public override Task<IEnumerable<StockItem>> GetFiltered(Expression<Func<StockItem, bool>>? filter = null, Func<IQueryable<StockItem>, IOrderedQueryable<StockItem>>? orderBy = null, int page = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        public override Task<StockItem> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InStock(Guid productId)
        {
            return await context.StockItems
                .AnyAsync(s => s.ProductId == productId && s.Quantity > 0);
        }

        public async Task<StockItem?> GetByProductIdAsync(Guid productId)
        {
            return await context.StockItems
                .Include(s => s.Product)
                .FirstOrDefaultAsync(s => s.ProductId == productId);
        }


        public override async Task<IEnumerable<StockItem>> MultipleByValue(IEnumerable<string> values)
        {
            return await context.StockItems
                .Include(p => p.Product)
                .Where(i => values.Contains(i.Product.SkuCode))
                .ToListAsync();
        }

        public override Task<int> CountAsync(Expression<Func<StockItem, bool>>? predicate = null)
        {
            throw new NotImplementedException();
        }

        public override Task<List<StockItem>> GetPagedAsync(int page, int pageSize, Expression<Func<StockItem, bool>>? filter = null, Func<IQueryable<StockItem>, IOrderedQueryable<StockItem>>? orderBy = null)
        {
            throw new NotImplementedException();
        }

        public override Task<bool> Exists(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
