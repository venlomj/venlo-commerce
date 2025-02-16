using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Repositories;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddDbContext<VenloCommerceDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("VenloCommerceConnection"));
            });

            return services;
        }
    }
}
