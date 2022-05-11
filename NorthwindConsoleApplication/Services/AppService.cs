using System;
using NorthwindConsoleApplication.Logger;
using NorthwindConsoleApplication.Model;
using NorthwindConsoleApplication.Services.IO;
using NorthwindConsoleApplication.Services.View;

namespace NorthwindConsoleApplication.Services
{
    public class AppService
    {
        private readonly ILoggerManager _logger;
        private readonly ConsoleView _view;
        
        public AppService(ILoggerManager logger, ConsoleView view)
        {
            _logger = logger;
            _view = view;
        }

        public void Start()
        {
            try
            {
                _logger.LogInfo("Application Started");

                // entry point 
                _view.StartMenu();

                _logger.LogInfo("Application Ended");
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
            }
        }
    }
}