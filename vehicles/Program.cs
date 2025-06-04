using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace vehicles
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        // Змінив IWebHostBuilder на IHostBuilder і CreateDefaultBuilder з Microsoft.Extensions.Hosting
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
