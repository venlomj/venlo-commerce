using Domain.Attributes.Interfaces;
using Domain.Documents;
using Domain.Repositories.Inventories;
using Domain.Repositories.Orders;
using Domain.Repositories.Pictures;
using Domain.Repositories.Products;
using Domain.Settings;
using Domain.UoW;
using Infrastructure.Persistence.SQL;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Inventories;
using Infrastructure.Repositories.Orders;
using Infrastructure.Repositories.Pictures;
using Infrastructure.Repositories.Products;
using Infrastructure.UoW;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductsReaderRepository, ProductsReaderRepository>();
            services.AddTransient<IProductWriterRepository, ProductWriterRepository>();
            services.AddTransient<IInventoriesReaderRepository, InventoriesReaderRepository>();
            services.AddTransient<IInventoriesWriterRepository, InventoriesWriterRepository>();
            services.AddTransient<IOrderWriterRepository, OrderWriterRepository>();
            services.AddTransient<IOrdersReaderRepository, OrdersReaderRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddDbContext<VenloCommerceDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("VenloCommerceConnection"));
            });

            // MongoDB Repositories
            services.AddTransient<IMongoRepository<ProductImage>, MongoRepository<ProductImage>>();

            return services;
        }
    }
}
