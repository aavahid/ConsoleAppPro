using System;
using Domain.Models;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductService _productRepository;

        public ProductService()
        {
            _productRepository = new ProductService();
        }

        public void Create(Product product)
        {
            _productRepository.Create(product);
        }

        public List<Product> GetAll()
        {
            return _productRepository.GetAll();
        }
    }
}

