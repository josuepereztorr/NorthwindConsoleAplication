using System;
using NorthwindConsoleApplication.Logger;

namespace NorthwindConsoleApplication.Services.IO
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
        
        public void PrintLnGreen(string text)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        public void PrintLnRed(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}