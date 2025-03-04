using System.Text;
using Application.Abstractions.Authentication;
using Domain.Documents;
using Domain.Repositories.Categories;
using Domain.Repositories.Inventories;
using Domain.Repositories.Orders;
using Domain.Repositories.Pictures;
using Domain.Repositories.Products;
using Domain.Repositories.Roles;
using Domain.Repositories.UserRoles;
using Domain.Repositories.Users;
using Domain.UoW;
using Infrastructure.Authentication;
using Infrastructure.Persistence.SQL;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Categories;
using Infrastructure.Repositories.Inventories;
using Infrastructure.Repositories.Orders;
using Infrastructure.Repositories.Pictures;
using Infrastructure.Repositories.Products;
using Infrastructure.Repositories.Roles;
using Infrastructure.Repositories.UserRoles;
using Infrastructure.Repositories.Users;
using Infrastructure.UoW;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

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
            services.AddTransient<ICategoryReaderRepository, CategoryReaderRepository>();
            services.AddTransient<IUserRolesWriterRepository, UserRolesWriterRepository>();
            services.AddTransient<IPasswordHasher, PasswordHasher>();
            services.AddTransient<IRolesReaderRepository, RolesReaderRepository>();
            services.AddTransient<IUserWriterRepository, UserWriterRepository>();
            services.AddTransient<IUserReaderRepository, UserReaderRepository>();
            services.AddTransient<ITokenProvider, TokenProvider>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddDbContext<VenloCommerceDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("VenloCommerceConnection"));
            });

            // MongoDB Repositories
            services.AddTransient<IMongoRepository<ProductImage>, MongoRepository<ProductImage>>();

            return services;
        }

        public static IServiceCollection AddAuthenticationInternal(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        ClockSkew = TimeSpan.Zero,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]!)),
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
                options.AddPolicy("VendorPolicy", policy => policy.RequireRole("Vendor"));
                options.AddPolicy("CustomerPolicy", policy => policy.RequireRole("Customer"));
                options.AddPolicy("GuestPolicy", policy => policy.RequireRole("Guest"));
            });

            //services.AddHttpContextAccessor();
            //services.AddScoped<IUserContext, UserContext>();
            //services.AddSingleton<IPasswordHasher, PasswordHasher>();
            //services.AddSingleton<ITokenProvider, TokenProvider>();

            return services;
        }
    }
}
