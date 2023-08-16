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
                ConsoleColor.DarkYellow.WriteConsole("Restaurant Menu:" +
                    "\n1. Create Restaurant" +
                    "\n2. List Restaurants" +
                    "\n3. Back");

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

        public void Create()
        {
            ConsoleColor.Cyan.WriteConsole("Add Restaurant Title: ");
            string title = Console.ReadLine();

            ConsoleColor.Cyan.WriteConsole("Add Restaurant Description: ");
            string description = Console.ReadLine();

            Location location = SelectExistingLocation(); 

            List<Product> products = CreateOrSelectProducts(); 

            Restaurant restaurant = new()
            {
                Title = title,
                Description = description,
                Location = location,
                Products = products
            };
            _restaurantService.Create(restaurant);

            ConsoleColor.Green.WriteConsole("Restaurant created successfully!");
        }

        public void GetAll()
        {
            foreach (Restaurant restaurant in _restaurantService.GetAll())
            {
                string restaurantData = $"id: {restaurant.Id}  Title: {restaurant.Title},  Description: {restaurant.Description}";

                ConsoleColor.DarkBlue.WriteConsole("Restaurant: " + restaurantData);
            }
        }
    }
}
