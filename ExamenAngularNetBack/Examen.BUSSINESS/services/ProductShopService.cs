using Examen.BUSSINESS.services.interfaces;
using Examen.DATA.Repositories.interfaces;
using Examen.ENTITY;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.BUSSINESS.services
{
    internal class ProductShopService : IProductShopService
    {

        private readonly IGenericRepository<ProductShop> _ProductShopRepository;

        public ProductShopService(IGenericRepository<ProductShop> productShopRepository)
        {
            _ProductShopRepository = productShopRepository;
        }


        public async Task<ProductShop> CreateProductShop(ProductShop newProductShop)
        {
            return await _ProductShopRepository.Insert(newProductShop);
        }

        public async Task<bool> DeleteProductShop(int productShopId)
        {
            var productShopToDelete = await _ProductShopRepository.GetOne(ps => ps.Id == productShopId);
            if (productShopToDelete == null)
                return false;

            return await _ProductShopRepository.Delete(productShopToDelete);
        }

    
        public async Task<IEnumerable<ProductShop>> GetAllProductShops()
        {
            var productShopsQuery = await _ProductShopRepository.GetAll();
            var productShops = productShopsQuery.Include(ps => ps.IdProductNavigation).Include(ps => ps.IdShopNavigation);
            return await productShops.ToListAsync();
        }
        

        public async Task<ProductShop> GetProductShopById(int productShopId)
        {
            return await _ProductShopRepository.GetOne(ps => ps.Id == productShopId);
        }

        public async Task<ProductShop> UpdateProductShop(ProductShop updatedProductShop)
        {
            return await _ProductShopRepository.Update(updatedProductShop);
        }
    }
}
