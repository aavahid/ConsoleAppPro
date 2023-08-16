using System;
using Domain.Models;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories
{
    public interface ILocationRepository : IBaseRepository<Location>
    {
        public void DeleteLocation();
    }
}

