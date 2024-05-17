using Examen.BUSSINESS.services.interfaces;
using Examen.DATA.Repositories.interfaces;
using Examen.ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Examen.BUSSINESS.services
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Product> _productRepository;

        public ProductService(IGenericRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> GetProductById(int productId)
        {
            return await _productRepository.GetOne(p => p.Id == productId);
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _productRepository.GetAll();
        }

        public async Task<Product> CreateProduct(Product newProduct)
        {
            return await _productRepository.Insert(newProduct);
        }

        public async Task<Product> UpdateProduct(Product updatedProduct)
        {
            return await _productRepository.Update(updatedProduct);
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            var productToDelete = await _productRepository.GetOne(p => p.Id == productId);
            if (productToDelete == null)
                return false;

            return await _productRepository.Delete(productToDelete);
        }
    }
}
