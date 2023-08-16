using System;
using Service.Services;
using Service.Services.Interfaces;
using Service.Helpers.Extentions;
using Domain.Models;

namespace ConsoleAppProject.Controllers
{
	public class LocationController
	{
		private readonly ILocationService _locationService;

		public LocationController()
		{
			_locationService = new LocationService();
		}

		public void Create()
		{
			ConsoleColor.Cyan.WriteConsole("Add Location Tittle");
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
        }


		public void GetAll()
		{
			foreach(Location location in _locationService.GetAll())
			{
				string data = $"id: {location.Id}  Title: {location.Title}  Latitude: {location.Latitude}  Longitude: {location.Longitude}";

				ConsoleColor.DarkBlue.WriteConsole("Information abaout location: " +data);
			}
		}
    }
}

