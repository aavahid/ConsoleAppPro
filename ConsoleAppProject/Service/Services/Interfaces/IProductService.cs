using System;
using Domain.Models;

namespace Service.Services.Interfaces
{
    public interface IProductService
    {
        void Create(Product product);
        List<Product> GetAll();
        Product GetById(int id);
        void Edit(Product product);
        void Delete(Product productToDelete);
    }
}
