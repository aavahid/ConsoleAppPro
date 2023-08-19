using System;
using Domain.Models;
using Repository.Data;

namespace Repository.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public void DeleteProduct(int id)
        {
            Product productToRemove = AppDbContext<Product>.datas.Find(p => p.Id == id);
            if (productToRemove != null)
            {
                AppDbContext<Product>.datas.Remove(productToRemove);
            }
        }

        public void EditProduct(Product product)
        {
            Product existingProduct = AppDbContext<Product>.datas.Find(p => p.Id == product.Id);
            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Description = product.Description;
                existingProduct.Price = product.Price;

            }
        }
    }
}

