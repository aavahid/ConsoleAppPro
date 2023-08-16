using System;
using Domain.Models;

namespace Service.Services.Interfaces
{
    public interface ILocationService
    {
        void Create(Location location);
        List<Location> GetAll();
    }
}

