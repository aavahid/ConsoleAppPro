using Domain.Models;
using Repository.Repositories;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class LocationService : ILocationService

    {
        private readonly ILocationRepository _locationRepository;
        private static int _count = 1;
        public LocationService()
        {
            _locationRepository = new LocationRepository();
        }

        public void Create(Location location)
        {
            location.Id = _count;
            _locationRepository.Create(location);
            _count++;
        }

        public List<Location> GetAll()
        {
            return _locationRepository.GetAll();
        }
    }
}

