using CommandLine;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace DiConsoleStarter
{
    public class MainWorker : IMainWorker
    {
        private readonly IConfiguration _configuration;

        private readonly ILogger<MainWorker> _logger;

        public MainWorker(IConfiguration configuration,
                          ILogger<MainWorker> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task ExecuteAsync(string[] args)
        {
            _logger.LogInformation("start {method}", nameof(ExecuteAsync));

            /// see: https://github.com/commandlineparser/commandline
            await Parser.Default.ParseArguments<Options>(args)
                   .WithParsedAsync(async o =>
                   {
                       if (o.Verbose)
                       {
                           _logger.LogInformation($"Verbose output enabled. Current Arguments: -v {o.Verbose}");
                           _logger.LogInformation("Quick Start Example! App is in Verbose mode!");
                       }
                       else
                       {
                           _logger.LogInformation($"Current Arguments: -v {o.Verbose}");
                           _logger.LogInformation("Quick Start Example!");
                       }

                       var pause = _configuration.GetValue<int>("pause", 1200);

                       await Task.Delay(pause);
                   });

            _logger.LogInformation("{method} finished!", nameof(ExecuteAsync));
        }
    }
}
