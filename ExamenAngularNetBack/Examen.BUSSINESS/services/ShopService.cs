using Examen.BUSSINESS.services.interfaces;
using Examen.DATA.Repositories.interfaces;
using Examen.ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.BUSSINESS.services
{
    public class ShopService : IShopSevice
    {
        private readonly IGenericRepository<Shop> _shopRepository;

        public ShopService(IGenericRepository<Shop> productRepository)
        {
            _shopRepository = productRepository;
        }

        public async Task<Shop> GetShopById(int shopId)
        {
            return await _shopRepository.GetOne(s => s.Id == shopId);
        }

        public async Task<Shop> CreateShop(Shop newShop)
        {
            return await _shopRepository.Insert(newShop);
        }

        public async Task<bool> DeleteShop(int shopId)
        {
            var shopToDelete = await _shopRepository.GetOne(s => s.Id == shopId);
            if (shopToDelete == null)
                return false;

            return await _shopRepository.Delete(shopToDelete);
        }

        public async Task<IEnumerable<Shop>> GetAllShops()
        {
            return await _shopRepository.GetAll();
        }

        public async Task<Shop> UpdateShop(Shop updatedShop)
        {
            return await _shopRepository.Update(updatedShop);
        }
    }
}
