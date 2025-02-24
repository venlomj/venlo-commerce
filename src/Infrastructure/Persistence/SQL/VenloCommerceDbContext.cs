using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.SQL;

public class VenloCommerceDbContext : DbContext
{
    public VenloCommerceDbContext(DbContextOptions<VenloCommerceDbContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; }
    public DbSet<StockItem> StockItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderLineItem> OrderLineItems { get; set; }
    public DbSet<Category> Categories { get; set; }
}
