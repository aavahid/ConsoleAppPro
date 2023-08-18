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

        public void Edit(Product product)
        {
            _productRepository.Edit(product);
        }

        public void Delete(int id)
        {
            Product product = _productRepository.GetById(id);
            if (product != null)
            {
                _productRepository.Delete(product);
            }
        }

        public Product GetById(int id)
        {
            return _productRepository.GetById(id);
        }

        public void Delete(Product productToDelete)
        {
            _productRepository.Delete(productToDelete);
        }
    }
}