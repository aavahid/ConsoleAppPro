using System;
using Domain.Models;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories
{
    public interface IProductRepository : IBaseRepository<Product>
    {
    }
}

