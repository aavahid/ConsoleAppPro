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

        public void Edit(Location location)
        {
            _locationRepository.Edit(location);
        }

        public void Delete(int id)
        {
            var location = _locationRepository.GetById(id);
            if (location != null)
            {
                _locationRepository.Delete(location);
            }
        }

        public Location GetById(int id)
        {
            return _locationRepository.GetById(id);
        }

        public void Delete(Location locationToDelete)
        {
            _locationRepository.Delete(locationToDelete);
        }
    }
}
