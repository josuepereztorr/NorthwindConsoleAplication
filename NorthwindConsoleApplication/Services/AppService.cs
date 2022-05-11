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
            _logger.LogInfo("Application Started");
            
            try
            {
                _view.StartMenu();

            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
            }
            
            _logger.LogInfo("Application Ended");
        }
    }
}