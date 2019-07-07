using DataPlus.Academic.Extensions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace DataPlus.Academic
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Run this sentence to run the SeedData method
            //CreateWebHostBuilder(args).Build().SeedData().Run();
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
