using System;
using Service.Services;
using Service.Services.Interfaces;
using Service.Helpers.Extentions;
using Domain.Models;

namespace ConsoleAppProject.Controllers
{
    public class ProductController
    {
        private readonly IProductService _productService;

        public ProductController()
        {
            _productService = new ProductService();
        }
        public void ProductMenu()
        {
            while (true)
            {
                ConsoleColor.DarkYellow.WriteConsole("Product Menu:\n1. Create Product\n2. List Products\n3. Back");

                string operation = Console.ReadLine();

                int operationNum;
                bool isTrueOperation = int.TryParse(operation, out operationNum);

                if (isTrueOperation)
                {
                    switch (operationNum)
                    {
                        case 1:
                            Create();
                            break;
                        case 2:
                            GetAll();
                            break;
                        case 3:
                            return; 
                        default:
                            ConsoleColor.Red.WriteConsole("Choose a correct operation:");
                            break;
                    }
                }
                else
                {
                    ConsoleColor.Red.WriteConsole("Choose a correct operation:");
                }
            }
        }

        public void Create()
        {
            string name;
            do
            {
                ConsoleColor.Cyan.WriteConsole("Add Product Name: ");
                name = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(name))
                {
                    ConsoleColor.Red.WriteConsole("Invalid input. Name cannot be empty. Please enter a valid name.");
                }
            } while (string.IsNullOrWhiteSpace(name));

            string description;
            do
            {
                ConsoleColor.Cyan.WriteConsole("Add Product Description: ");
                description = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(description))
                {
                    ConsoleColor.Red.WriteConsole("Invalid input. Description cannot be empty. Please enter a valid description.");
                }
            } while (string.IsNullOrWhiteSpace(description));


            decimal price;
            while (true)
            {
                ConsoleColor.Cyan.WriteConsole("Add Product Price");
                if (decimal.TryParse(Console.ReadLine(), out price))
                {
                    break;
                }
                else
                {
                    ConsoleColor.Red.WriteConsole("Invalid input. Please enter a valid decimal value for Price.");
                }
            }

            Product product = new()
            {
                Name = name,
                Description = description,
                Price = price
            };
            _productService.Create(product);

            ConsoleColor.Green.WriteConsole("Location created is Successfully!");
        }


        public void GetAll()
        {
            foreach (Product product in _productService.GetAll())
            {
                string productData = $"id: {product.Id}  Name: {product.Name},  Description: {product.Description}, Price: {product.Price}";

                ConsoleColor.DarkBlue.WriteConsole("Product: " + productData);
            }
        }
    }
}

