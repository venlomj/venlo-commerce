using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Domain.Constants;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Persistence.SQL;

public class VenloCommerceDbContext : DbContext
{
    public VenloCommerceDbContext(DbContextOptions<VenloCommerceDbContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; }
    public DbSet<StockItem> StockItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderLineItem> OrderLineItems { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Role>().HasData(
            new {Id = RoleConstants.AdminId, Name = RoleConstants.Admin, DateCreated = DateTimeOffset.UtcNow },
            new {Id = RoleConstants.VendorId, Name = RoleConstants.Vendor, DateCreated = DateTimeOffset.UtcNow },
            new {Id = RoleConstants.CustomerId, Name = RoleConstants.Customer, DateCreated = DateTimeOffset.UtcNow },
            new {Id = RoleConstants.GuestId, Name = RoleConstants.Guest, DateCreated = DateTimeOffset.UtcNow }
            );
    }
}
