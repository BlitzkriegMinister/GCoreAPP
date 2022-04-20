using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;

namespace GCoreApp
{
    public class Program
    {
        public static string token = "";
        public static List<Models.GetVideoResponse> videos = new List<Models.GetVideoResponse>();
        public static bool Authorize = false;
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
