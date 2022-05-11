using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NorthwindConsoleApplication.Logger;
using NorthwindConsoleApplication.Model;
using NorthwindConsoleApplication.Services;
using NorthwindConsoleApplication.Services.Database;
using NorthwindConsoleApplication.Services.IO;
using NorthwindConsoleApplication.Services.View;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace NorthwindConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = CreateDefaultBuilder(args);
            var app = GetAppService(host.Services);
            app.Start();
        }
        
        private static ILoggerManager GetLoggerManager(IServiceProvider serviceProvider)
        {
            return serviceProvider.GetRequiredService<ILoggerManager>();
        }
        
        private static AppService GetAppService(IServiceProvider serviceProvider)
        {
            return serviceProvider.GetRequiredService<AppService>();
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
                    services.AddSingleton<ILoggerManager, LoggerManagerService>();
                    services.AddSingleton<DatabaseService>();
                    services.AddSingleton<ConsoleInputService>();
                    services.AddSingleton<ConsoleOutputService>();
                    services.AddSingleton<AppService>();
                    services.AddSingleton<ConsoleView>();
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
    }
}