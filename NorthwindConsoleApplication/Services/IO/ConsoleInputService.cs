using System;
using NorthwindConsoleApplication.Logger;

namespace NorthwindConsoleApplication.Services.IO
{
    public class ConsoleInputService
    {
        private readonly ILoggerManager _logger;

        public ConsoleInputService(ILoggerManager logger)
        {
            _logger = logger;
        }
        
        public string GetInput()
        {
            string input = Console.ReadLine();
            
            // todo check if input is null or empty
            
            return input;
        }
    }
}