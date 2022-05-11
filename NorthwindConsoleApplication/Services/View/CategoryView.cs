using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NorthwindConsoleApplication.Logger;
using NorthwindConsoleApplication.Model;
using NorthwindConsoleApplication.Services.Database;
using NorthwindConsoleApplication.Services.IO;

namespace NorthwindConsoleApplication.Services.View
{
    public class CategoryView
    {
        private readonly DatabaseService _database;
        private readonly ILoggerManager _logger;
        private readonly ConsoleInputService _input;
        private readonly ConsoleOutputService _output;

        public CategoryView(DatabaseService database, ILoggerManager logger, ConsoleInputService input, ConsoleOutputService output)
        {
            _database = database;
            _logger = logger;
            _input = input;
            _output = output;
        }
        
        public void CreateCategory()
        {
            var category = new Category();
            
            _output.PrintLn("Create Category");
            _output.PrintLn("---------------");
            
            _output.Print("Category Name: ");
            var name = _input.GetInputString();
            category.CategoryName = name;
            
            _output.Print("Description: ");
            var description = _input.GetInputString();
            category.Description = description;

            _database.Add(category);
            _database.Save();
            
            _logger.LogInfo($"{category.CategoryName} added to database");
        }
        
        public void ReadCategory()
        {
            _output.PrintLn("Read Category Records");
            _output.PrintLn("---------------------");

            _output.PrintLn("Select a option:");
            _output.PrintLn("1) All Categories");
            _output.PrintLn("2) All Categories and Active Products");
            _output.PrintLn("3) Category By ID");

            var input = _input.GetInputString();
            _output.PrintLn("");
            
            if (string.IsNullOrWhiteSpace(input) || !new[] {"1","2","3"}.Contains(input))
            {
                _logger.LogInfo("Invalid Selection");
            }

            switch (input)
            {
                case "1":
                    AllCategories();
                    break;
                case "2":
                    AllCategoriesAndActiveProducts();
                    break;
                case "3":
                    CategoryByIdAndActiveProducts();
                    break;
            }
        }

        public void UpdateCategory()
        {
            _output.PrintLn("Read Product Records");
            _output.PrintLn("--------------------");
            _output.Print("Enter Category ID: ");
            
            var id = _input.GetInputInteger();
            var categoryById = _database.GetById<Category>(id);
            var propertyId = "";
            
            do
            {
                _output.PrintLn("Select the property to update :");
                _output.PrintLn("1) Category ID");
                _output.PrintLn("2) Name");
                _output.PrintLn("3) Description");
                _output.PrintLn("Enter q to to return to main menu");

                propertyId = _input.GetInputString();
                
                if (propertyId != "q")
                    _output.Print("Enter new data value: ");

                switch (propertyId)
                {
                    case "1":
                        categoryById.CategoryId = _input.GetInputInteger();
                        _database.Save();
                        break;
                    case "2":
                        categoryById.CategoryName = _input.GetInputString();
                        _database.Save();
                        break;
                    case "3":
                        categoryById.Description = _input.GetInputString();
                        _database.Save();
                        break;
                }
            } while (propertyId != "q");
        }
        
        private void AllCategories()
        {
            var allCategories = _database.GetAllQueryable<Category>().ToList();

            _output.PrintLn("");

            _output.PrintLn("All Categories");
            _output.PrintLn("------------");
            allCategories.ForEach(p =>
            {
                _output.PrintLn($"Name: {p.CategoryName}");
                _output.PrintLn($"Description: {p.Description}");
            });
            
        }

        private void AllCategoriesAndActiveProducts()
        {
            var active = _database.GetAllQueryable<Category>().Include(category => category.Products.Where(product => !product.Discontinued))
                .ToList();
            
            _output.PrintLn("");
            _output.PrintLn("All Categories and Active Products");
            _output.PrintLn("--------------------------------");
            
            active.ForEach(c =>
            {
                _output.PrintLn($"Name: {c.CategoryName}");
                _output.PrintLn($"Products: ");
                foreach (var product in c.Products)
                {
                    _output.PrintLn(product.ProductName);
                }
                _output.PrintLn("");
            });
        }

        private void CategoryByIdAndActiveProducts()
        {
            _output.PrintLn("Read Category Records");
            _output.PrintLn("---------------------");
            _output.Print("Enter Category ID: ");
            
            var id = _input.GetInputInteger();
            var activeById = _database.GetAllQueryable<Category>().Where(category => category.CategoryId.Equals(id)).Include(category => category.Products.Where(product => !product.Discontinued)).ToList();

            _output.PrintLn("");
            _output.PrintLn("Category By ID and Active Products");
            _output.PrintLn("--------------------------------");

            activeById.ForEach(c =>
            {
                _output.PrintLn($"Name: {c.CategoryName}");
                _output.PrintLn($"Products: ");
                foreach (var product in c.Products)
                {
                    _output.PrintLn(product.ProductName);
                }
                _output.PrintLn("");
            });
        }
    }
}