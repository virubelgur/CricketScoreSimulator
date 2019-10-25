using System;
using Domain;
using Domain.Contract;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleApp
{
    public class Program
    {
        private static IServiceProvider _serviceProvider;

        static void Main()
        {
            RegisterServices();

            AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionTrapper;

            var scoreService = _serviceProvider.GetService<ICricketScoreService>();
            var configService = _serviceProvider.GetService<IConfigurationReader>();
            var matchConfiguration = configService.GetMatchConfiguration("appsettings.json");
            var scoreCard = scoreService.GetScoreCard(matchConfiguration);

            var display = new DisplayScoreCard(scoreCard, matchConfiguration);
            display.FullScoreCard();
            
            DisposeServices();

            Console.WriteLine("\n\nPress Enter to Exit");
            Console.ReadLine();
            Environment.Exit(0);

        }
        /// <summary>
        /// Registers Dependency for services
        /// </summary>
        private static void RegisterServices()
        {
            var collection = new ServiceCollection();
            collection.AddScoped<ICricketScoreService, CricketScoreService>();
            collection.AddScoped<IConfigurationReader, ConfigurationReader>();
            _serviceProvider = collection.BuildServiceProvider();
        }

        /// <summary>
        /// Dispose services at the end of application
        /// </summary>
        private static void DisposeServices()
        {
            if (_serviceProvider == null)
            {
                return;
            }
            if (_serviceProvider is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }

        /// <summary>
        /// Allow users to come out of application in case of unhandled exception
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void UnhandledExceptionTrapper(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine(e.ExceptionObject);
            Console.WriteLine("\nPlease check your match configurations & re-try");
            Console.WriteLine("\nPress Enter to Exit");
            Console.ReadLine();
            Environment.Exit(0);
        }
    }

}
