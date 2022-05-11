using System.Linq;
using NorthwindConsoleApplication.Logger;
using NorthwindConsoleApplication.Services.IO;


namespace NorthwindConsoleApplication.Services.View
{
    public class ConsoleView
    {
        private readonly ILoggerManager _logger;
        private readonly ConsoleInputService _input;
        private readonly ConsoleOutputService _output;
        private readonly ProductView _productView;
        private readonly CategoryView _categoryView;

        public ConsoleView(ILoggerManager logger, ConsoleInputService input, ConsoleOutputService output, ProductView productView, CategoryView categoryView)
        {
            _logger = logger;
            _input = input;
            _output = output;
            _productView = productView;
            _categoryView = categoryView;
        }

        public void StartMenu()
        {
            string tableSelection;
            
            do
            {
                // Show menu
                ShowHeader();
                ShowTables();

                tableSelection = _input.GetInputString();
                
                if (string.IsNullOrWhiteSpace(tableSelection) || !new[] {"1","2"}.Contains(tableSelection))
                {
                    _logger.LogInfo("Invalid Selection");
                    continue;
                }

                if (tableSelection == "1")
                    _logger.LogInfo("Option '1' selected");
                else if (tableSelection == "2")
                    _logger.LogInfo("Option '2' selected");
                
                // Chose operation 
                ShowCrudOperations();

                var crudSelection = _input.GetInputString();
                
                if (string.IsNullOrWhiteSpace(crudSelection) || !new[] {"1","2","3","4"}.Contains(crudSelection))
                {
                    _logger.LogInfo("Invalid Selection");
                    continue;
                }

                switch (crudSelection)
                {
                    case "1":
                        ShowCreate(tableSelection);
                        break;
                    case "2":
                        ShowRead(tableSelection);
                        break;
                    case "3":
                        ShowUpdate(tableSelection);
                        break;
                    case "4" :
                        ShowDelete(tableSelection);
                        break;
                }
                
            } while (tableSelection != null && !tableSelection.Equals("q"));
        }

        private void ShowHeader()
        {
            _output.PrintLn("Northwind Database Console Application");
            _output.PrintLn("--------------------------------------");
        }

        private void ShowTables()
        {
            _output.PrintLn("Select a table to query or perform a command:");
            _output.PrintLn("1) Products");
            _output.PrintLn("2) Categories");
            _output.PrintLn("Enter q to quit application");
        }
        
        private void ShowCrudOperations()
        {
            _output.PrintLn("Enter your selection:");
            _output.PrintLn("1) Create Record");
            _output.PrintLn("2) Read Record");
            _output.PrintLn("3) Update Record");
            _output.PrintLn("4) Delete Record");
        }

        private void ShowCreate(string table)
        {
            _logger.LogInfo("Option '1' selected");
            if (table == "1")
                _productView.CreateProduct();
            else
                _categoryView.CreateCategory();

            _output.PrintLn("");
        }

        private void ShowRead(string table)
        {
            _logger.LogInfo("Option '2' selected");
            if (table == "1")
                _productView.ReadProduct();
            else
                _categoryView.ReadCategory();

            _output.PrintLn("");
        }

        private void ShowUpdate(string table)
        {
            _logger.LogInfo("Option '3' selected");
            if (table == "1")
                _productView.UpdateProduct();
            else
                _categoryView.UpdateCategory();

            _output.PrintLn("");
        }

        private void ShowDelete(string table)
        {
            _logger.LogInfo("Option '4' selected");
        }
    }
}