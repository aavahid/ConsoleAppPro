using System;
using Domain.Models;

namespace Repository.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly List<Location> _locations = new List<Location>();
        private int _nextLocationId = 1;

        static void AddNewLocation()
        {
            Console.WriteLine("Adding a new location");

            // Gather location information
            Console.Write("Enter title: ");
            string title = Console.ReadLine();
            Console.Write("Enter latitude: ");
            double latitude = Convert.ToDouble(Console.ReadLine());
            Console.Write("Enter longitude: ");
            double longitude = Convert.ToDouble(Console.ReadLine());

            // Create the new location
            Location newLocation = new Location
            {
                Id = locations.Count + 1,
                Title = title,
                Latitude = latitude,
                Longitude = longitude
            };

            // Add the location to the list
            locations.Add(newLocation);

            Console.WriteLine("Location added successfully!");
        }

        static void ListLocations()
        {
            Console.WriteLine("Listing all locations");

            foreach (var location in locations)
            {
                Console.WriteLine($"Location ID: {location.Id}");
                Console.WriteLine($"Title: {location.Title}");
                Console.WriteLine($"Latitude: {location.Latitude}");
                Console.WriteLine($"Longitude: {location.Longitude}");
                Console.WriteLine();
            }
        }

        public Location AddLocation(Location location)
        {
            throw new NotImplementedException();
        }

        public List<Location> GetAllLocations()
        {
            throw new NotImplementedException();
        }

        public Location GetLocationById(int id)
        {
            throw new NotImplementedException();
        }
    }

}