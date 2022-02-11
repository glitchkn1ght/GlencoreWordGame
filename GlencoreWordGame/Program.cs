using GlencoreWordGame.Application;
using GlencoreWordGame.Config;
using GlencoreWordGame.Scoring;
using GlencoreWordGame.Service;
using GlencoreWordGame.Validation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.IO;
using System.Threading.Tasks;

namespace GlencoreWordGame
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);

            // create service provider
            var serviceProvider = services.BuildServiceProvider();

            // entry to run app
            await serviceProvider.GetService<WordGameApplication>().RunGame(args);
        }
    
        private static void ConfigureServices(IServiceCollection services)
        {
            // build config
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .AddEnvironmentVariables()
            .Build();

            // configure logging
            services.AddLogging(builder => builder.AddSerilog(
                new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger()))
                .BuildServiceProvider();

            //Configure game and api settings
            services.Configure<ApiSettings>(configuration.GetSection("ApiSettings"));
            services.Configure<GameSettings>(configuration.GetSection("GameSettings"));

            // add services:
            services.AddHttpClient<IWordGameService>();
            services.AddTransient<IWordGameService, WordGameService>();
            services.AddTransient<IUserInputValidator, UserInputValidator>();
            services.AddTransient<IWordGameOrchestrator, WordGameOrchestrator>();
            services.AddTransient<IWordGameScorer, WordGameScorer>();

            // add app
            services.AddTransient<WordGameApplication>();
        }
    }
}
