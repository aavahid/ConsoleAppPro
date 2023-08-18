using System;
using System.Collections.Generic;
using Domain.Models;
using Service.Services;
using Service.Services.Interfaces;
using Service.Helpers.Extentions;

namespace ConsoleAppProject.Controllers
{
    public class RestaurantController
    {
        private readonly IRestaurantService _restaurantService;
        private readonly ILocationService _locationService;
        private readonly IProductService _productService;

        public RestaurantController()
        {
            _restaurantService = new RestaurantService();
            _locationService = new LocationService();
            _productService = new ProductService();
        }

        public void RestaurantMenu()
        {
            while (true)
            {
                ConsoleColor.DarkYellow.WriteConsole("Restaurant Operations:" +
                "\n1. Create Restaurant" +
                "\n2. Edit Restaurants" +
                "\n3. Delete Restaurants" +
                "\n4. Open Menu" +
                "\n5. List Restaurants" +
                "\n6. Back to Main Operations");


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
                            break;
                        case 3:
                            Delete();
                            break;
                        case 4:
                            OpenMenu();
                            break;
                        case 5:
                            GetAll();
                            break;
                        case 6:
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
            ConsoleColor.Cyan.WriteConsole("Add Restaurant Title: ");
            string title = Console.ReadLine();

            ConsoleColor.Cyan.WriteConsole("Add Restaurant Description: ");
            string description = Console.ReadLine();

            Location location = SelectExistingLocation();

            List<Product> products = new List<Product>();
            while (true)
            {
                ConsoleColor.Green.WriteConsole("Add Product? (Y/N)");
                if (Console.ReadLine().Trim().Equals("N", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }

                ConsoleColor.Green.WriteConsole("Select an existing product or create a new one? (E/C)");
                string choice = Console.ReadLine().Trim();

                if (choice.Equals("E", StringComparison.OrdinalIgnoreCase))
                {
                    Product existingProduct = SelectExistingProduct();
                    if (existingProduct != null)
                    {
                        if (products.Exists(p => p.Id == existingProduct.Id))
                        {
                            ConsoleColor.Yellow.WriteConsole("This product is already associated with the restaurant.");
                            ConsoleColor.Green.WriteConsole("1. End product selection");
                            ConsoleColor.Green.WriteConsole("2. Resume product selection");
                            string endOrResume = Console.ReadLine().Trim();
                            if (endOrResume == "1")
                            {
                                break;
                            }
                            else if (endOrResume == "2")
                            {
                                continue;
                            }
                            else
                            {
                                ConsoleColor.Red.WriteConsole("Invalid choice. Please choose 1 or 2.");
                            }
                        }
                        else
                        {
                            products.Add(existingProduct);
                        }
                    }
                }
                else if (choice.Equals("C", StringComparison.OrdinalIgnoreCase))
                {
                    products.Add(CreateProduct());
                }
                else
                {
                    ConsoleColor.Red.WriteConsole("Invalid choice. Please choose E or C.");
                }
            }

            ConsoleColor.Green.WriteConsole("Choose Menu Type: ");
            ConsoleColor.Green.WriteConsole("1. Fastfood Menu");
            ConsoleColor.Green.WriteConsole("2. Regular Restaurant Menu");

            string menuChoice = Console.ReadLine().Trim();
            string menuType;

            switch (menuChoice)
            {
                case "1":
                    menuType = "Fastfood Menu";
                    break;
                case "2":
                    menuType = "Regular Restaurant Menu";
                    break;
                default:
                    menuType = "Unknown Menu Type";
                    break;
            }

            Restaurant restaurant = new()
            {
                Title = title,
                Description = description,
                Location = location,
                Products = products,
                MenuType = menuType
            };
            _restaurantService.Create(restaurant);

            ConsoleColor.Green.WriteConsole("Restaurant created successfully!");

            DisplayProducts(restaurant.Products);
        }


        private Location SelectExistingLocation()
        {

            return SelectLocationFromList();
        }

        private Location SelectLocationFromList()
        {
            List<Location> allLocations = _locationService.GetAll();
            List<Location> availableLocations = new List<Location>(allLocations);


            foreach (Restaurant restaurant in _restaurantService.GetAll())
            {
                availableLocations.RemoveAll(location => location.Id == restaurant.Location.Id);
            }

            if (availableLocations.Count == 0)
            {
                ConsoleColor.Yellow.WriteConsole("All available locations are already associated with a restaurant. You will need to create a new location.");
                return CreateLocation();
            }

            ConsoleColor.DarkYellow.WriteConsole("Select an available location:");
            for (int i = 0; i < availableLocations.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {availableLocations[i].Title}");
            }

            int selectedIndex;
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out selectedIndex) && selectedIndex >= 1 && selectedIndex <= availableLocations.Count)
                {
                    return availableLocations[selectedIndex - 1];
                }
                else
                {
                    ConsoleColor.Red.WriteConsole("Invalid selection. Please enter a valid option.");
                }
            }
        }

        private Location CreateLocation()
        {
            ConsoleColor.Cyan.WriteConsole("Add Location Title: ");
            string title = Console.ReadLine();

            double latitude;
            while (true)
            {
                ConsoleColor.Cyan.WriteConsole("Add Location Latitude");
                if (double.TryParse(Console.ReadLine(), out latitude))
                {
                    break;
                }
                else
                {
                    ConsoleColor.Red.WriteConsole("Invalid input. Please enter a valid double value for Latitude.");
                }
            }

            double longitude;
            while (true)
            {
                ConsoleColor.Cyan.WriteConsole("Add Location Longitude");
                if (double.TryParse(Console.ReadLine(), out longitude))
                {
                    break;
                }
                else
                {
                    ConsoleColor.Red.WriteConsole("Invalid input. Please enter a valid double value for Longitude.");
                }
            }

            Location location = new()
            {
                Title = title,
                Latitude = latitude,
                Longitude = longitude
            };
            _locationService.Create(location);

            ConsoleColor.Green.WriteConsole("Location created is Successfully!");

            return location;
        }

        private List<Product> CreateOrSelectProducts()
        {


            List<Product> products = new List<Product>();

            while (true)
            {
                ConsoleColor.Cyan.WriteConsole("Add Product Name: ");
                string name = Console.ReadLine();

                ConsoleColor.Cyan.WriteConsole("Add Product Description: ");
                string description = Console.ReadLine();

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
                products.Add(product);

                ConsoleColor.Green.WriteConsole("Product created is Successfully! Add another product? (Y/N)");

                if (Console.ReadLine().Trim().Equals("N", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }
            }

            return products;
        }

        private Product SelectExistingProduct()
        {
            ConsoleColor.DarkYellow.WriteConsole("Select an existing product:");
            foreach (Product product in _productService.GetAll())
            {
                ConsoleColor.DarkBlue.WriteConsole($"id: {product.Id}  Name: {product.Name},  Description: {product.Description}, Price: {product.Price}");
            }

            ConsoleColor.Cyan.WriteConsole("Enter the ID of the product to associate:");
            if (int.TryParse(Console.ReadLine(), out int productId))
            {
                return _productService.GetById(productId);
            }
            else
            {
                ConsoleColor.Red.WriteConsole("Invalid input. Please enter a valid product ID.");
                return null;
            }
        }

        private Product CreateProduct()
        {
            ConsoleColor.Cyan.WriteConsole("Add Product Name: ");
            string name = Console.ReadLine();

            ConsoleColor.Cyan.WriteConsole("Add Product Description: ");
            string description = Console.ReadLine();

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

            return product;
        }

        private void DisplayProducts(List<Product> products)
        {
            ConsoleColor.DarkYellow.WriteConsole("Products associated with the restaurant:");
            foreach (Product product in products)
            {
                ConsoleColor.DarkBlue.WriteConsole($"id: {product.Id}  Name: {product.Name},  Description: {product.Description}, Price: {product.Price}");
            }
        }

        public void Edit()
        {
            ConsoleColor.Cyan.WriteConsole("Enter Restaurant ID to edit:");
            if (int.TryParse(Console.ReadLine(), out int restaurantId))
            {
                Restaurant restaurant = _restaurantService.GetById(restaurantId);
                if (restaurant == null)
                {
                    ConsoleColor.Red.WriteConsole("Restaurant not found.");
                    return;
                }


                ConsoleColor.DarkYellow.WriteConsole($"Restaurant founded:" +
                    $"\n1. Editing Restaurant with ID {restaurant.Id}:" +
                    $"\n2. Current Title: {restaurant.Title}:" +
                    $"\n3. Current Description: {restaurant.Description}:" +
                    $"\n4. Current Location: {restaurant.Location}");

                ConsoleColor.Cyan.WriteConsole("Enter new Restaurant Title:");
                string newTitle = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newTitle))
                {
                    restaurant.Title = newTitle;
                }

                ConsoleColor.Cyan.WriteConsole("Enter new Restaurant Description:");
                string newDescription = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newDescription))
                {
                    restaurant.Description = newDescription;
                }

                Location location = restaurant.Location;
                ConsoleColor.Cyan.WriteConsole("Enter new Location Latitude:");
                string latitudeInput = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(latitudeInput) && double.TryParse(latitudeInput, out double newLatitude))
                {
                    location.Latitude = newLatitude;
                }

                ConsoleColor.Cyan.WriteConsole("Enter new Location Longitude:");
                string longitudeInput = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(longitudeInput) && double.TryParse(longitudeInput, out double newLongitude))
                {
                    location.Longitude = newLongitude;
                }

                restaurant.Location = location;

                _restaurantService.Edit(restaurant);
                ConsoleColor.Green.WriteConsole("Restaurant updated successfully!");
            }
            else
            {
                ConsoleColor.Red.WriteConsole("Invalid input. Please enter a valid Restaurant ID.");
            }
        }

        public void Delete()
        {
            ConsoleColor.Cyan.WriteConsole("Enter Restaurant ID to delete:");
            if (int.TryParse(Console.ReadLine(), out int restaurantId))
            {
                Restaurant restaurantToDelete = _restaurantService.GetById(restaurantId);
                if (restaurantToDelete != null)
                {
                    _restaurantService.Delete(restaurantToDelete);
                    ConsoleColor.Green.WriteConsole("Restaurant deleted successfully!");
                }
                else
                {
                    ConsoleColor.Red.WriteConsole("Restaurant not found.");
                }
            }
            else
            {
                ConsoleColor.Red.WriteConsole("Invalid input. Please enter a valid restaurant ID.");
            }
        }

        public void GetAll()
        {
            foreach (Restaurant restaurant in _restaurantService.GetAll())
            {
                string restaurantData = $"id: {restaurant.Id}  Title: {restaurant.Title},  Description: {restaurant.Description}";

                ConsoleColor.DarkBlue.WriteConsole("Restaurant: " + restaurantData);
            }
        }

        public void OpenMenu()
        {
            ConsoleColor.Cyan.WriteConsole("Enter Restaurant ID to open menu:");
            if (int.TryParse(Console.ReadLine(), out int restaurantId))
            {
                Restaurant restaurant = _restaurantService.GetById(restaurantId);
                if (restaurant != null)
                {
                    ConsoleColor.Green.WriteConsole($"Menu for Restaurant {restaurant.Title}:");

                    if (restaurant.MenuType == "Fastfood Menu")
                    {
                        ConsoleColor.DarkBlue.WriteConsole("Fastfood Menu:");
                        DisplayMenuItems(restaurant.Products);
                    }
                    else if (restaurant.MenuType == "Regular Restaurant Menu")
                    {
                        ConsoleColor.DarkBlue.WriteConsole("Restaurant Menu:");
                        DisplayMenuItems(restaurant.Products);
                    }
                    else
                    {
                        ConsoleColor.Yellow.WriteConsole("No menu items available.");
                    }

                    ConsoleColor.Green.WriteConsole("\nPress any key to continue...");
                    Console.ReadKey();
                }
                else
                {
                    ConsoleColor.Red.WriteConsole("Restaurant not found.");
                }
            }
            else
            {
                ConsoleColor.Red.WriteConsole("Invalid input. Please enter a valid restaurant ID.");
            }
        }

        private void DisplayMenuItems(List<Product> menuItems)
        {
            foreach (Product menuItem in menuItems)
            {
                ConsoleColor.DarkBlue.WriteConsole($"Item: {menuItem.Name}, Description: {menuItem.Description}, Price: {menuItem.Price}");
            }
        }
    }
}
