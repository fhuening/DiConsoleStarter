using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DiConsoleStarter
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Create DI Container
            var services = new ServiceCollection();

            // Configure dependencies
            ConfigureServices(services);

            // Start main worker
            await services
                .AddSingleton<IMainWorker, MainWorker>()
                .BuildServiceProvider()
                .GetService<IMainWorker>()
                .ExecuteAsync(args);
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            // Build configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();

            // Create logger
            var logger = new LoggerConfiguration()
                .ReadFrom
                .Configuration(configuration)
                .CreateLogger();

            // Add services
            services
                .AddSingleton<IConfiguration>(configuration)
                .AddLogging(configure =>
                {
                    configure
                        .AddSerilog(logger);
                });
        }
    }
}
