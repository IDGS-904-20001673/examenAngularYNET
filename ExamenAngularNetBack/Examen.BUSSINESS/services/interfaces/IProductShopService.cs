using Examen.ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.BUSSINESS.services.interfaces
{
    public interface IProductShopService
    {
        Task<ProductShop> GetProductShopById(int productShopId);
        Task<IEnumerable<ProductShop>> GetAllProductShops();
        Task<ProductShop> CreateProductShop(ProductShop newProductShop);
        Task<ProductShop> UpdateProductShop(ProductShop updatedProductShop);
        Task<bool> DeleteProductShop(int productShopId);

    }
}
