using Domain.Models;
using Repository.Data;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories
{
    public class LocationRepository : BaseRepository<Location>, ILocationRepository
    {
        public void DeleteLocation(int id)
        {
            Location locationToRemove = AppDbContext<Location>.datas.Find(l => l.Id == id);
            if (locationToRemove != null)
            {
                AppDbContext<Location>.datas.Remove(locationToRemove);
            }
        }
        public void EditLocation(Location location)
        {
            Location existingLocation = AppDbContext<Location>.datas.Find(l => l.Id == location.Id);
            if (existingLocation != null)
            {
                existingLocation.Title = location.Title;
                existingLocation.Latitude = location.Latitude;
                existingLocation.Longitude = location.Longitude;
            
            }
        }
    }
}