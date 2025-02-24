using Infrastructure.Persistence.SQL;

namespace Api.Extensions
{
    public static class HostExtensions
    {
        public static async Task SeedDatabaseAsync(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            await DbSeeder.SeedAsync(services);
        }
    }
}
