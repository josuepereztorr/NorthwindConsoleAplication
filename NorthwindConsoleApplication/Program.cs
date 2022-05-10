using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NorthwindConsoleApplication.Logger;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace NorthwindConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = CreateDefaultBuilder(args);
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
                    services.AddLogging(logger =>
                    {
                        logger.ClearProviders();
                        logger.SetMinimumLevel(LogLevel.Information);
                    });
                })
                .Build();
        }
    }
}