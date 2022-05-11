using System;
using NorthwindConsoleApplication.Logger;

namespace NorthwindConsoleApplication.IO
{
    public class ConsoleOutputService
    {
        private readonly ILoggerManager _logger;

        public ConsoleOutputService(ILoggerManager logger)
        {
            _logger = logger;
        }
        
        public void Print(string text)
        {
            Console.Write(text);
        }

        public void PrintLn(string text)
        {
            Console.WriteLine(text);
        }
    }
}