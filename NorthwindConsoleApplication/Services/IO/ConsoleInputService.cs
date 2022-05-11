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

        public string GetInputString()
        {
            var input = Console.ReadLine();
            return input;
        }
        
        public int GetInputInteger()
        {
            var input = Convert.ToInt32(Console.ReadLine());
            return input;
        }
        
        public decimal GetInputDecimal()
        {
            var input = Convert.ToDecimal(Console.ReadLine());
            return input;
        }
        
        public short GetInputShort()
        {
            var input = Convert.ToInt16(Console.ReadLine());
            return input;
        }
        
        public bool GetInputBool()
        {
            var input = Convert.ToBoolean(Console.ReadLine().ToLower());
            return input;
        }
        
    }
}