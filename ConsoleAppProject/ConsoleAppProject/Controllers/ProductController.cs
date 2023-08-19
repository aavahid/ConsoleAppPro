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
                ConsoleColor.DarkYellow.WriteConsole("Product Operations:" +
                    "\n1. Create Product" +
                    "\n2. Edit Product" +
                    "\n3. Delete Product" +
                    "\n4. List Products" +
                    "\n5. Back to Main Operations");

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
                            Edit();
                            return;
                        case 3:
                            Delete();
                            return;
                        case 4:
                            GetAll();
                            break;
                        case 5:
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

            ConsoleColor.Green.WriteConsole("Product created is Successfully!");
        }

        public void GetAll()
        {
            var products = _productService.GetAll();

            if (products.Count == 0)
            {
                ConsoleColor.DarkRed.WriteConsole("Product list is empty.");
                return;
            }

            foreach (Product product in products)
            {
                string productData = $"id: {product.Id}  Name: {product.Name},  Description: {product.Description}, Price: {product.Price}";

                ConsoleColor.DarkBlue.WriteConsole("Product: " + productData);
            }
        }


        public void Edit()
        {
            ConsoleColor.Cyan.WriteConsole("Enter Product ID to edit:");
            if (int.TryParse(Console.ReadLine(), out int productId))
            {
                Product product = _productService.GetById(productId);
                if (product == null)
                {
                    ConsoleColor.Red.WriteConsole("Location not found.");
                    return;
                }


                ConsoleColor.DarkYellow.WriteConsole($"Product found:" +
                    $"\n1. Editing Product with ID {product.Id}:" +
                    $"\n2. Current Name: {product.Name}:" +
                    $"\n3. Current Description: {product.Description}:" +
                    $"\n4. Current Price: {product.Price}");

                ConsoleColor.Cyan.WriteConsole("Enter new Product Name:");
                string newName = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newName))
                {
                    product.Name = newName;
                }

                ConsoleColor.Cyan.WriteConsole("Enter new Product Description:");
                string newDescription = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newDescription))
                {
                    product.Description = newDescription;
                }

                ConsoleColor.Cyan.WriteConsole("Enter new Product Price:");
                string priceInput = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(priceInput) && decimal.TryParse(priceInput, out decimal newPrice))
                {
                    product.Price = newPrice;
                }

                _productService.Edit(product);
                ConsoleColor.Green.WriteConsole("Product updated successfully!");
            }
            else
            {
                ConsoleColor.Red.WriteConsole("Invalid input. Please enter a valid product ID.");
            }
        }

        public void Delete()
        {
            ConsoleColor.Cyan.WriteConsole("Enter Product ID to delete:");
            if (int.TryParse(Console.ReadLine(), out int productId))
            {
                Product productToDelete = _productService.GetById(productId);
                if (productToDelete != null)
                {
                    _productService.Delete(productToDelete);
                    ConsoleColor.Green.WriteConsole("Product deleted successfully!");
                }
                else
                {
                    ConsoleColor.Red.WriteConsole("Product not found.");
                }
            }
            else
            {
                ConsoleColor.Red.WriteConsole("Invalid input. Please enter a valid product ID.");
            }
        }

    }
}
