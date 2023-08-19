using Domain.Models;
using Service.Helpers.Extentions;
using Service.Services;
using Service.Services.Interfaces;

namespace ConsoleAppProject.Controllers
{
    public class LocationController
    {
        private readonly ILocationService _locationService;

        public LocationController()
        {
            _locationService = new LocationService();
        }
        public void LocationMenu()
        {
            while (true)
            {
                ConsoleColor.DarkYellow.WriteConsole("Location Operations:" +
                    "\n1. Create Location" +
                    "\n2. Edit Location" +
                    "\n3. Delete Location" +
                    "\n4. List Locations" +
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
                            break;
                        case 3:
                            Delete();
                            return;
                        case 4:
                            GetAll();
                            return;
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
            ConsoleColor.Cyan.WriteConsole("Add Location Title");
            string title;
            do
            {
                title = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(title))
                {
                    ConsoleColor.Red.WriteConsole("Invalid input. Title cannot be empty. Please enter a valid title.");
                }
            } while (string.IsNullOrWhiteSpace(title));

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
        }

        public void GetAll()
        {
            var locations = _locationService.GetAll();

            if (locations.Count == 0)
            {
                ConsoleColor.DarkRed.WriteConsole("Location list is empty.");
                return;
            }

            foreach (Location location in locations)
            {
                string data = $"id: {location.Id}  Title: {location.Title}  Latitude: {location.Latitude}  Longitude: {location.Longitude}";

                ConsoleColor.DarkBlue.WriteConsole("Location: " + data);
            }
        }


        public void Edit()
        {
            ConsoleColor.Cyan.WriteConsole("Enter Location ID to edit:");
            if (int.TryParse(Console.ReadLine(), out int locationId))
            {
                Location location = _locationService.GetById(locationId);
                if (location == null)
                {
                    ConsoleColor.Red.WriteConsole("Location not found.");
                    return;
                }


                ConsoleColor.DarkYellow.WriteConsole($"Location found:" +
                    $"\n1. Editing Location with ID {location.Id}:" +
                    $"\n2. Current Title: {location.Title}:" +
                    $"\n3. Current Latitude: {location.Latitude}:" +
                    $"\n4. Current Longitude: {location.Longitude}");

                ConsoleColor.Cyan.WriteConsole("Enter new Location Title:");
                string newTitle = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newTitle))
                {
                    location.Title = newTitle;
                }

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

                _locationService.Edit(location);
                ConsoleColor.Green.WriteConsole("Location updated successfully!");
            }
            else
            {
                ConsoleColor.Red.WriteConsole("Invalid input. Please enter a valid location ID.");
            }
        }

        public void Delete()
        {
            ConsoleColor.Cyan.WriteConsole("Enter Location ID to delete:");
            if (int.TryParse(Console.ReadLine(), out int locationId))
            {
                Location locationToDelete = _locationService.GetById(locationId);
                if (locationToDelete != null)
                {
                    _locationService.Delete(locationToDelete);
                    ConsoleColor.Green.WriteConsole("Location deleted successfully!");
                }
                else
                {
                    ConsoleColor.Red.WriteConsole("Location not found.");
                }
            }
            else
            {
                ConsoleColor.Red.WriteConsole("Invalid input. Please enter a valid location ID.");
            }
        }
    }
}
