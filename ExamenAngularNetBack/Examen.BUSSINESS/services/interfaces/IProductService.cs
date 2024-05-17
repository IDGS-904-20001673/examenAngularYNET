using Examen.ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.BUSSINESS.services.interfaces
{
    public interface IProductService
    {
        Task<Product> GetProductById(int productId);
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> CreateProduct(Product newProduct);
        Task<Product> UpdateProduct(Product updatedProduct);
        Task<bool> DeleteProduct(int productId);
    }
}
