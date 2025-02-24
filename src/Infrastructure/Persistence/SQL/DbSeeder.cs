using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistence.SQL
{
    public class DbSeeder
    {
        public static async Task SeedAsync(IServiceProvider service)
        {
            using (var context = service.GetRequiredService<VenloCommerceDbContext>())
            {
                // Ensure the database is created
                await context.Database.EnsureCreatedAsync();

                if (!context.Categories.Any())
                {
                    context.Categories.AddRange(
                        new Category
                        {
                            Id = Guid.NewGuid(),
                            Name = "Electronics",
                            Description = "Electronic gadgets and devices",
                            DateCreated = DateTimeOffset.UtcNow,
                            IsDeleted = false
                        },
                        new Category
                        {
                            Id = Guid.NewGuid(),
                            Name = "Clothing",
                            Description = "Apparel and accessories",
                            DateCreated = DateTimeOffset.UtcNow,
                            IsDeleted = false
                        },
                        new Category
                        {
                            Id = Guid.NewGuid(),
                            Name = "Home & Garden",
                            Description = "Furniture, tools, and home appliances",
                            DateCreated = DateTimeOffset.UtcNow,
                            IsDeleted = false
                        }
                    );
                    await context.SaveChangesAsync();
                }

                if (!context.Products.Any())
                {
                    var electronicsCategoryId = context.Categories.First(c => c.Name == "Electronics").Id;
                    var clothingCategoryId = context.Categories.First(c => c.Name == "Clothing").Id;
                    var homeAndGardenCategoryId = context.Categories.First(c => c.Name == "Home & Garden").Id;

                    context.Products.AddRange(
                        new Product { Id = Guid.NewGuid(), SkuCode = "SKU001", Name = "Smartphone", Description = "Latest model smartphone with high-definition camera and long battery life.", Price = 499.99m, CategoryId = electronicsCategoryId, DateCreated = DateTimeOffset.UtcNow, IsDeleted = false },
                        new Product { Id = Guid.NewGuid(), SkuCode = "SKU002", Name = "Laptop", Description = "High-performance laptop for work and gaming with 16GB RAM.", Price = 1299.99m, CategoryId = electronicsCategoryId, DateCreated = DateTimeOffset.UtcNow, IsDeleted = false },
                        new Product { Id = Guid.NewGuid(), SkuCode = "SKU003", Name = "Blender", Description = "High-speed blender for making smoothies and soups.", Price = 89.99m, CategoryId = homeAndGardenCategoryId, DateCreated = DateTimeOffset.UtcNow, IsDeleted = false },
                        new Product { Id = Guid.NewGuid(), SkuCode = "SKU004", Name = "Wireless Headphones", Description = "Noise-canceling wireless headphones with 30-hour battery life.", Price = 199.99m, CategoryId = electronicsCategoryId, DateCreated = DateTimeOffset.UtcNow, IsDeleted = false },
                        new Product { Id = Guid.NewGuid(), SkuCode = "SKU005", Name = "T-shirt", Description = "Cotton t-shirt with a comfortable fit and cool graphic design.", Price = 19.99m, CategoryId = clothingCategoryId, DateCreated = DateTimeOffset.UtcNow, IsDeleted = false },
                        new Product { Id = Guid.NewGuid(), SkuCode = "SKU006", Name = "Jacket", Description = "Stylish winter jacket with a water-resistant outer shell.", Price = 89.99m, CategoryId = clothingCategoryId, DateCreated = DateTimeOffset.UtcNow, IsDeleted = false },
                        new Product { Id = Guid.NewGuid(), SkuCode = "SKU007", Name = "Electric Heater", Description = "Compact electric heater perfect for small rooms or office spaces.", Price = 49.99m, CategoryId = homeAndGardenCategoryId, DateCreated = DateTimeOffset.UtcNow, IsDeleted = false },
                        new Product { Id = Guid.NewGuid(), SkuCode = "SKU008", Name = "Coffee Machine", Description = "Automatic coffee machine with a built-in grinder and programmable features.", Price = 149.99m, CategoryId = homeAndGardenCategoryId, DateCreated = DateTimeOffset.UtcNow, IsDeleted = false },
                        new Product { Id = Guid.NewGuid(), SkuCode = "SKU009", Name = "Gaming Console", Description = "Next-gen gaming console with ultra-fast load times and 4K support.", Price = 499.99m, CategoryId = electronicsCategoryId, DateCreated = DateTimeOffset.UtcNow, IsDeleted = false },
                        new Product { Id = Guid.NewGuid(), SkuCode = "SKU010", Name = "Smartwatch", Description = "Feature-rich smartwatch with heart rate monitor and GPS.", Price = 199.99m, CategoryId = electronicsCategoryId, DateCreated = DateTimeOffset.UtcNow, IsDeleted = false },
                        new Product { Id = Guid.NewGuid(), SkuCode = "SKU011", Name = "Sunglasses", Description = "Polarized sunglasses for sunny days.", Price = 39.99m, CategoryId = clothingCategoryId, DateCreated = DateTimeOffset.UtcNow, IsDeleted = false },
                        new Product { Id = Guid.NewGuid(), SkuCode = "SKU012", Name = "Bluetooth Speaker", Description = "Portable Bluetooth speaker with excellent sound quality.", Price = 69.99m, CategoryId = electronicsCategoryId, DateCreated = DateTimeOffset.UtcNow, IsDeleted = false },
                        new Product { Id = Guid.NewGuid(), SkuCode = "SKU013", Name = "Smart TV", Description = "4K UHD Smart TV with streaming apps and voice control.", Price = 799.99m, CategoryId = electronicsCategoryId, DateCreated = DateTimeOffset.UtcNow, IsDeleted = false },
                        new Product { Id = Guid.NewGuid(), SkuCode = "SKU014", Name = "Air Fryer", Description = "Healthy air fryer for crispy fries without the oil.", Price = 99.99m, CategoryId = homeAndGardenCategoryId, DateCreated = DateTimeOffset.UtcNow, IsDeleted = false },
                        new Product { Id = Guid.NewGuid(), SkuCode = "SKU015", Name = "Gaming Mouse", Description = "Precision gaming mouse with customizable buttons and RGB lighting.", Price = 49.99m, CategoryId = electronicsCategoryId, DateCreated = DateTimeOffset.UtcNow, IsDeleted = false },
                        new Product { Id = Guid.NewGuid(), SkuCode = "SKU016", Name = "Electric Toothbrush", Description = "Rechargeable electric toothbrush with advanced cleaning modes.", Price = 89.99m, CategoryId = electronicsCategoryId, DateCreated = DateTimeOffset.UtcNow, IsDeleted = false },
                        new Product { Id = Guid.NewGuid(), SkuCode = "SKU017", Name = "Electric Kettle", Description = "Fast boiling electric kettle with automatic shut-off feature.", Price = 29.99m, CategoryId = homeAndGardenCategoryId, DateCreated = DateTimeOffset.UtcNow, IsDeleted = false },
                        new Product { Id = Guid.NewGuid(), SkuCode = "SKU018", Name = "Frying Pan", Description = "Durable non-stick frying pan for easy cooking.", Price = 25.99m, CategoryId = homeAndGardenCategoryId, DateCreated = DateTimeOffset.UtcNow, IsDeleted = false },
                        new Product { Id = Guid.NewGuid(), SkuCode = "SKU019", Name = "Smart Thermostat", Description = "Smart thermostat for controlling home temperature from anywhere.", Price = 119.99m, CategoryId = homeAndGardenCategoryId, DateCreated = DateTimeOffset.UtcNow, IsDeleted = false },
                        new Product { Id = Guid.NewGuid(), SkuCode = "SKU020", Name = "Portable Charger", Description = "Compact power bank for charging devices on the go.", Price = 29.99m, CategoryId = electronicsCategoryId, DateCreated = DateTimeOffset.UtcNow, IsDeleted = false },
                        new Product { Id = Guid.NewGuid(), SkuCode = "SKU021", Name = "Designer Jeans", Description = "Premium quality denim jeans with a stylish cut.", Price = 69.99m, CategoryId = clothingCategoryId, DateCreated = DateTimeOffset.UtcNow, IsDeleted = false },
                        new Product { Id = Guid.NewGuid(), SkuCode = "SKU022", Name = "Wireless Charger", Description = "Qi-compatible wireless charger for fast charging of smartphones.", Price = 39.99m, CategoryId = electronicsCategoryId, DateCreated = DateTimeOffset.UtcNow, IsDeleted = false },
                        new Product { Id = Guid.NewGuid(), SkuCode = "SKU023", Name = "Office Chair", Description = "Ergonomic office chair with lumbar support and adjustable features.", Price = 159.99m, CategoryId = homeAndGardenCategoryId, DateCreated = DateTimeOffset.UtcNow, IsDeleted = false },
                        new Product { Id = Guid.NewGuid(), SkuCode = "SKU024", Name = "Running Shoes", Description = "Comfortable running shoes designed for long-distance runs.", Price = 79.99m, CategoryId = clothingCategoryId, DateCreated = DateTimeOffset.UtcNow, IsDeleted = false },
                        new Product { Id = Guid.NewGuid(), SkuCode = "SKU025", Name = "Laptop Bag", Description = "Stylish and functional bag for carrying laptops and accessories.", Price = 49.99m, CategoryId = clothingCategoryId, DateCreated = DateTimeOffset.UtcNow, IsDeleted = false },
                        new Product { Id = Guid.NewGuid(), SkuCode = "SKU026", Name = "Smartphone Case", Description = "Protective case for your smartphone with a sleek design.", Price = 19.99m, CategoryId = electronicsCategoryId, DateCreated = DateTimeOffset.UtcNow, IsDeleted = false },
                        new Product { Id = Guid.NewGuid(), SkuCode = "SKU027", Name = "Robot Vacuum", Description = "Smart robot vacuum cleaner with automatic cleaning mode.", Price = 299.99m, CategoryId = homeAndGardenCategoryId, DateCreated = DateTimeOffset.UtcNow, IsDeleted = false },
                        new Product { Id = Guid.NewGuid(), SkuCode = "SKU028", Name = "Wall Clock", Description = "Modern wall clock with minimalist design.", Price = 29.99m, CategoryId = homeAndGardenCategoryId, DateCreated = DateTimeOffset.UtcNow, IsDeleted = false },
                        new Product { Id = Guid.NewGuid(), SkuCode = "SKU029", Name = "Backpack", Description = "Durable backpack with multiple compartments for all your essentials.", Price = 49.99m, CategoryId = clothingCategoryId, DateCreated = DateTimeOffset.UtcNow, IsDeleted = false },
                        new Product { Id = Guid.NewGuid(), SkuCode = "SKU030", Name = "Telescope", Description = "Beginner-friendly telescope for stargazing.", Price = 159.99m, CategoryId = electronicsCategoryId, DateCreated = DateTimeOffset.UtcNow, IsDeleted = false }
                    );
                    await context.SaveChangesAsync();
                }

                if (!context.Orders.Any())
                {
                    context.Orders.AddRange(
                        new Order
                        {
                            Id = Guid.NewGuid(),
                            OrderNumber = "ORD001",
                            DateCreated = DateTimeOffset.UtcNow,
                            IsDeleted = false
                        },
                        new Order
                        {
                            Id = Guid.NewGuid(),
                            OrderNumber = "ORD002",
                            DateCreated = DateTimeOffset.UtcNow,
                            IsDeleted = false
                        }
                    );
                    await context.SaveChangesAsync();
                }

                if (!context.StockItems.Any())
                {
                    var product1Id = context.Products.First(p => p.Name == "Smartphone").Id;
                    var product2Id = context.Products.First(p => p.Name == "Laptop").Id;

                    context.StockItems.AddRange(
                        new StockItem
                        {
                            Id = Guid.NewGuid(),
                            Quantity = 50,
                            ProductId = product1Id,
                            DateCreated = DateTimeOffset.UtcNow,
                            IsDeleted = false
                        },
                        new StockItem
                        {
                            Id = Guid.NewGuid(),
                            Quantity = 30,
                            ProductId = product2Id,
                            DateCreated = DateTimeOffset.UtcNow,
                            IsDeleted = false
                        }
                    );
                    await context.SaveChangesAsync();
                }

            }
        }
    }
}
