using System;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NorthwindConsoleApplication.Logger;
using NorthwindConsoleApplication.Model;
using NorthwindConsoleApplication.Services;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace NorthwindConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = CreateDefaultBuilder(args);
            var logger = GetLoggerManager(host.Services);
            var dbService = GetDatabaseService(host.Services);
            
            try
            {
                logger.LogInfo("Application Started");

                // testing DatabaseService 
                var aProduct = dbService.GetById<Product>(1);
                Console.WriteLine(aProduct.ProductName);

                logger.LogInfo("Application Ended");
            }
            catch (Exception exception)
            {
                logger.LogError(exception.Message);
            }
        }

        private static IHost CreateDefaultBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((_, builder) =>
                {
                    builder.SetBasePath(Directory.GetCurrentDirectory());
                    builder.AddJsonFile("appsettings.json", true, true);
                    builder.Build();
                    LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton<ILoggerManager, LoggerManager>();
                    services.AddSingleton<DatabaseService>();
                    services.AddLogging(logger =>
                    {
                        logger.ClearProviders();
                        logger.SetMinimumLevel(LogLevel.Information);
                    });
                    services.AddDbContext<NWConsole_48_JPTContext>(options =>
                    {
                        options.UseSqlServer(context.Configuration.GetConnectionString("DbConnection"));
                    });
                })
                .Build();
        }
        
        private static ILoggerManager GetLoggerManager(IServiceProvider serviceProvider)
        {
            return serviceProvider.GetRequiredService<ILoggerManager>();
        }
        
        private static DatabaseService GetDatabaseService(IServiceProvider serviceProvider)
        {
            return serviceProvider.GetRequiredService<DatabaseService>();
        }
    }
}