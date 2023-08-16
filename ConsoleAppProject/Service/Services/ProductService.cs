using System;
using Domain.Models;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductService _productRepository;
        private int _count = 1;

        public ProductService()
        {
            _productRepository = new ProductService();
        }

        public void Create(Product product)
        {
            product.Id = _count;
            _productRepository.Create(product);
            _count++;
        }

        public List<Product> GetAll()
        {
            return _productRepository.GetAll();
        }
    }
}

