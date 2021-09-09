using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Linq;

namespace RedBrain.Authentication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
            .Build()
            .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    IConfigurationRoot config = new ConfigurationBuilder()
                        .AddCommandLine(args)
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .Build();

                    //fetching localhost url from config
                    config.Providers.FirstOrDefault(x => x.GetType() == typeof(JsonConfigurationProvider)).TryGet("AppSettings:LocalHost", out string url);

                    webBuilder
                    .UseConfiguration(config)
                    .UseKestrel()
                    .UseContentRoot(Directory.GetCurrentDirectory())
                    .UseIISIntegration()
                    .UseStartup<Startup>()
                    .UseUrls(url);
                });
    }
}
