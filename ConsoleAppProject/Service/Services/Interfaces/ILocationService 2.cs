using System;
using Domain.Models;

namespace Service.Services.Interfaces
{
    public interface ILocationService
    {
        void Create(Location location);
        void Delete(int id);
        Location GetById(int id);
        List<Location> GetAll();
        void Edit(Location location);
        void Delete(Location locationToDelete);
    }
}