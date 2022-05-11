using System.Linq;
using Microsoft.EntityFrameworkCore;
using NorthwindConsoleApplication.Logger;
using NorthwindConsoleApplication.Model;
using NorthwindConsoleApplication.Services.Database;
using NorthwindConsoleApplication.Services.IO;

namespace NorthwindConsoleApplication.Services.View
{
    public class ProductView
    {
        private readonly DatabaseService _database;
        private readonly ILoggerManager _logger;
        private readonly ConsoleInputService _input;
        private readonly ConsoleOutputService _output;

        public ProductView(DatabaseService database, ILoggerManager logger, ConsoleInputService input, ConsoleOutputService output)
        {
            _database = database;
            _logger = logger;
            _input = input;
            _output = output;
        }
        
        public void CreateProduct()
        {
            var product = new Product();
            
            _output.PrintLn("Create Product");
            _output.PrintLn("-----------");
            
            _output.Print("Product Name: ");
            var name = _input.GetInputString();
            product.ProductName = name;
            
            _output.Print("Supplier ID: ");
            var supplierId = _input.GetInputInteger();
            product.SupplierId = supplierId;

            _output.Print("Category ID: ");
            var categoryId = _input.GetInputInteger();
            product.CategoryId = categoryId;

            _output.Print("Quantity Per Unit: ");
            var quantityPerUnit = _input.GetInputString();
            product.QuantityPerUnit = quantityPerUnit;
            
            _output.Print("Unit Price: ");
            var unitPrice = _input.GetInputDecimal();
            product.UnitPrice = unitPrice;
            
            _output.Print("Units In Stock: ");
            var unitsInStock = _input.GetInputShort();
            product.UnitsInStock = unitsInStock;
            
            _output.Print("Units On Order: ");
            var unitsOnOrder = _input.GetInputShort();
            product.UnitsOnOrder = unitsOnOrder;
            
            _output.Print("Reorder Level: ");
            var reorderLevel = _input.GetInputShort();
            product.ReorderLevel = reorderLevel;
            
            _output.Print("Discontinued (true/false): ");
            var discontinued = _input.GetInputBool();
            product.Discontinued = discontinued;

            _database.Add(product);
            _database.Save();
        }

        public void ReadProduct()
        {
            _output.PrintLn("Read Product Records");
            _output.PrintLn("---------------------");

            _output.PrintLn("Select a option:");
            _output.PrintLn("1) All Products");
            _output.PrintLn("2) Active Products");
            _output.PrintLn("3) Discontinued Products");
            _output.PrintLn("4) Product by ID");

            var input = _input.GetInputString();
            _output.PrintLn("");

            switch (input)
            {
                case "1":
                    ReadAllProducts();
                    break;
                case "2":
                    ReadActiveProducts();
                    break;
                case "3":
                    ReadDiscontinuedProducts();
                    break;
                case "4" :
                    ReadProductById();
                    break;
            }
        }

        public void UpdateProduct()
        {
            _output.PrintLn("Update Product Records");
            _output.PrintLn("--------------------");
            _output.Print("Enter Product ID: ");
            
            var id = _input.GetInputInteger();
            var productById = _database.GetById<Product>(id);
            var propertyId = "";
            
            do
            {
                _output.PrintLn("Select the property to update :");
                _output.PrintLn("1) Product ID");
                _output.PrintLn("2) Product Name");
                _output.PrintLn("3) Supplier ID");
                _output.PrintLn("4) Category ID");
                _output.PrintLn("5) Quantity Per Unit");
                _output.PrintLn("6) Unit Price");
                _output.PrintLn("7) Units In Stock");
                _output.PrintLn("8) Units On Order");
                _output.PrintLn("9) Reorder Level");
                _output.PrintLn("10) Discontinued");
                _output.PrintLn("Enter q to to return to main menu");

                propertyId = _input.GetInputString();
                
                if (propertyId != "q")
                    _output.Print("Enter new data value: ");

                switch (propertyId)
                {
                    case "1":
                        productById.ProductId = _input.GetInputInteger();
                        _database.Save();
                        break;
                    case "2":
                        productById.ProductName = _input.GetInputString();
                        _database.Save();
                        break;
                    case "3":
                        productById.SupplierId = _input.GetInputInteger();
                        _database.Save();
                        break;
                    case "4":
                        productById.CategoryId = _input.GetInputInteger();
                        _database.Save();
                        break;
                    case "5":
                        productById.QuantityPerUnit = _input.GetInputString();
                        _database.Save();
                        break;
                    case "6":
                        productById.UnitPrice = _input.GetInputDecimal();
                        _database.Save();
                        break;
                    case "7":
                        productById.UnitsInStock = _input.GetInputShort();
                        _database.Save();
                        break;
                    case "8":
                        productById.UnitsOnOrder = _input.GetInputShort();
                        _database.Save();
                        break;
                    case "9":
                        productById.ReorderLevel = _input.GetInputShort();
                        _database.Save();
                        break;
                    case "10":
                        productById.Discontinued = _input.GetInputBool();
                        _database.Save();
                        break;
                    
                }
            } while (propertyId != "q");
        }

        public void DeleteProduct()
        {
            
        }

        private void ReadAllProducts()
        {
            ReadActiveProducts();
            ReadDiscontinuedProducts();
        }

        private void ReadActiveProducts()
        {
            var active =  _database.GetAllQueryable<Product>().Where(p => !p.Discontinued).ToList();
            
            _output.PrintLn("");

            _output.PrintLn("Active Products");
            _output.PrintLn("---------------");
            active.ForEach( p => _output.PrintLnGreen(p.ProductName));
        }

        private void ReadDiscontinuedProducts()
        {
            var discontinued = _database.GetAllQueryable<Product>().Where(p => p.Discontinued).ToList();
            
            _output.PrintLn("");
            
            _output.PrintLn("Discontinued Products");
            _output.PrintLn("---------------------");
            discontinued.ForEach(p => _output.PrintLnRed(p.ProductName));
        }
        
        private void ReadProductById()
        {
            _output.PrintLn("Read Product Records");
            _output.PrintLn("---------------------");
            _output.Print("Enter Product ID: ");
            
            var id = _input.GetInputInteger();
            var p = _database.GetById<Product>(id);

            _output.PrintLn("");

            _output.PrintLn("Product By ID");
            _output.PrintLn("-------------");
            _output.PrintLn($"ID: {p.ProductId}");
            _output.PrintLn($"Name: {p.ProductName}");
            _output.PrintLn($"Supplier ID: {p.SupplierId}");
            _output.PrintLn($"Category ID: {p.CategoryId}");
            _output.PrintLn($"Quantity Per Unit: {p.QuantityPerUnit}");
            _output.PrintLn($"UnitPrice: {p.UnitPrice}");
            _output.PrintLn($"Units In Stock: {p.UnitsInStock}");
            _output.PrintLn($"Units On Order: {p.UnitsOnOrder}");
            _output.PrintLn($"Reorder Level: {p.ReorderLevel}");
            _output.PrintLn($"Discontinued: {p.Discontinued}");

            _output.PrintLn("");
        }
    }
}