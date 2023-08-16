using Domain.Models;
using Repository.Repositories;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class LocationService : ILocationService

    {
        private readonly ILocationRepository _locationRepository;

        public LocationService()
        {
            _locationRepository = new LocationRepository();
        }

        public void Create(Location location)
        {
            _locationRepository.Create(location);
        }

        public List<Location> GetAll()
        {
            return _locationRepository.GetAll();
        }
    }
}

