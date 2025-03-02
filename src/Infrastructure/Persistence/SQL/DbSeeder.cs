using Domain.Entities;
using Infrastructure.Persistence.SQL;
using Microsoft.Extensions.DependencyInjection;

public class DbSeeder
{
    public static async Task SeedAsync(IServiceProvider service)
    {
        await using var context = service.GetRequiredService<VenloCommerceDbContext>();
        await context.Database.EnsureCreatedAsync();

        if (!context.Categories.Any())
        {
            var categories = new List<Category>
            {
                new() { Name = "Electronics", Description = "Electronic gadgets and devices" },
                new() { Name = "Clothing", Description = "Apparel and accessories" },
                new() { Name = "Home & Kitchen", Description = "Household and kitchen items" },
                new() { Name = "Beauty & Personal Care", Description = "Cosmetics and grooming products" },
                new() { Name = "Sports & Outdoors", Description = "Sporting goods and outdoor gear" },
                new() { Name = "Books", Description = "Various books and literature" }
            };
            context.Categories.AddRange(categories);
            await context.SaveChangesAsync();
        }

        if (!context.Products.Any())
        {
            var categories = context.Categories.ToList();
            var products = new List<Product>
            {
                new() { Id = Guid.NewGuid(), SkuCode = "SKU-0001", Name = "Wireless Earbuds", Description = "Noise-canceling Bluetooth earbuds", Price = 79.99m, CategoryId = categories[0].Id },
                new() { Id = Guid.NewGuid(), SkuCode = "SKU-0002", Name = "Smartwatch", Description = "Fitness tracking smartwatch", Price = 199.99m, CategoryId = categories[0].Id },
                new() { Id = Guid.NewGuid(), SkuCode = "SKU-0003", Name = "Laptop", Description = "High-performance laptop with 16GB RAM", Price = 999.99m, CategoryId = categories[0].Id },
                new() { Id = Guid.NewGuid(), SkuCode = "SKU-0004", Name = "Gaming Mouse", Description = "RGB gaming mouse with adjustable DPI", Price = 49.99m, CategoryId = categories[0].Id },
                new() { Id = Guid.NewGuid(), SkuCode = "SKU-0005", Name = "Running Shoes", Description = "Breathable running shoes for men", Price = 89.99m, CategoryId = categories[1].Id },
                new() { Id = Guid.NewGuid(), SkuCode = "SKU-0006", Name = "Winter Jacket", Description = "Waterproof insulated jacket", Price = 129.99m, CategoryId = categories[1].Id },
                new() { Id = Guid.NewGuid(), SkuCode = "SKU-0007", Name = "Leather Wallet", Description = "Premium leather wallet with card slots", Price = 39.99m, CategoryId = categories[1].Id },
                new() { Id = Guid.NewGuid(), SkuCode = "SKU-0008", Name = "Coffee Maker", Description = "Automatic drip coffee maker", Price = 59.99m, CategoryId = categories[2].Id },
                new() { Id = Guid.NewGuid(), SkuCode = "SKU-0009", Name = "Blender", Description = "High-speed blender with glass jar", Price = 89.99m, CategoryId = categories[2].Id },
                new() { Id = Guid.NewGuid(), SkuCode = "SKU-0010", Name = "Vacuum Cleaner", Description = "Cordless vacuum cleaner with strong suction", Price = 159.99m, CategoryId = categories[2].Id },
                new() { Id = Guid.NewGuid(), SkuCode = "SKU-0011", Name = "Face Moisturizer", Description = "Hydrating face cream with SPF", Price = 24.99m, CategoryId = categories[3].Id },
                new() { Id = Guid.NewGuid(), SkuCode = "SKU-0012", Name = "Shampoo & Conditioner Set", Description = "Organic hair care duo", Price = 34.99m, CategoryId = categories[3].Id },
                new() { Id = Guid.NewGuid(), SkuCode = "SKU-0013", Name = "Dumbbells", Description = "Adjustable dumbbells set", Price = 99.99m, CategoryId = categories[4].Id },
                new() { Id = Guid.NewGuid(), SkuCode = "SKU-0014", Name = "Yoga Mat", Description = "Non-slip yoga mat with carrying strap", Price = 29.99m, CategoryId = categories[4].Id },
                new() { Id = Guid.NewGuid(), SkuCode = "SKU-0015", Name = "Trekking Backpack", Description = "Waterproof hiking backpack", Price = 69.99m, CategoryId = categories[4].Id },
                new() { Id = Guid.NewGuid(), SkuCode = "SKU-0016", Name = "Fiction Novel", Description = "Bestselling fiction book", Price = 14.99m, CategoryId = categories[5].Id },
                new() { Id = Guid.NewGuid(), SkuCode = "SKU-0017", Name = "Cookbook", Description = "Recipes from around the world", Price = 19.99m, CategoryId = categories[5].Id },
                new() { Id = Guid.NewGuid(), SkuCode = "SKU-0018", Name = "Wireless Speaker", Description = "Portable Bluetooth speaker", Price = 49.99m, CategoryId = categories[0].Id },
                new() { Id = Guid.NewGuid(), SkuCode = "SKU-0019", Name = "Gaming Keyboard", Description = "Mechanical gaming keyboard", Price = 79.99m, CategoryId = categories[0].Id },
                new() { Id = Guid.NewGuid(), SkuCode = "SKU-0020", Name = "Smart TV", Description = "55-inch 4K UHD Smart TV", Price = 599.99m, CategoryId = categories[0].Id },
                new() { Id = Guid.NewGuid(), SkuCode = "SKU-0021", Name = "Electric Kettle", Description = "Fast-boiling electric kettle", Price = 39.99m, CategoryId = categories[2].Id },
                new() { Id = Guid.NewGuid(), SkuCode = "SKU-0022", Name = "Lipstick Set", Description = "Matte lipstick collection", Price = 29.99m, CategoryId = categories[3].Id },
                new() { Id = Guid.NewGuid(), SkuCode = "SKU-0023", Name = "Basketball", Description = "Official size and weight basketball", Price = 24.99m, CategoryId = categories[4].Id },
                new() { Id = Guid.NewGuid(), SkuCode = "SKU-0024", Name = "Biography Book", Description = "Inspiring true story", Price = 16.99m, CategoryId = categories[5].Id },
                new() { Id = Guid.NewGuid(), SkuCode = "SKU-0025", Name = "T-Shirt", Description = "Cotton unisex t-shirt", Price = 19.99m, CategoryId = categories[1].Id }
            };
            context.Products.AddRange(products);
            await context.SaveChangesAsync();
        }
    }
}
