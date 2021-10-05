using WJBPokerApp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace WJBPokerApp
{
    public class Program

    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        // This is for the EF, if using. Leave it here, we'll add it programmatically later.
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

