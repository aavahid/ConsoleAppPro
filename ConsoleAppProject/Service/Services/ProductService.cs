using Domain.Models;
using Repository.Repositories;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class ProductService : IProductService

    {
        private readonly IProductRepository _productRepository;
        private static int _count = 1;
        public ProductService()
        {
            _productRepository = new ProductRepository();
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

