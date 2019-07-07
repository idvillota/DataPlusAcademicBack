using DataPlus.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataPlus.Academic.Extensions
{
    public static class WebHostExtensions
    {
        public static IWebHost SeedData(this IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetService<RepositoryContext>();
                
                context.Database.Migrate();

                new DataSeeder(context).SeedData();

            }

            return null;
        }
    }
}
