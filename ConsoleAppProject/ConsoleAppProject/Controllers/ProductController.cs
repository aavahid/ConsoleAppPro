using System;
using Service.Services;
using Service.Services.Interfaces;

namespace ConsoleAppProject.Controllers
{
	public class ProductController
	{
		private readonly IProductService _productService;
		public ProductController()
		{
			_productService = new ProductService();
		}
	}
}

