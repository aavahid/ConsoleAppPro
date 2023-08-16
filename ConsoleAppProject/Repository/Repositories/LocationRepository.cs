using Domain.Models;
using Repository.Data;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories
{
    public class LocationRepository : BaseRepository<Location>, ILocationRepository
    {
        public void DeleteLocation()
        {
            throw new NotImplementedException();
        }
    }
}

