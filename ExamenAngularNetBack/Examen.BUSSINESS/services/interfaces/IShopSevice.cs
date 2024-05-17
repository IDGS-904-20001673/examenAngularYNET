using Examen.ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.BUSSINESS.services.interfaces
{
    public interface IShopSevice
    {
        Task<Shop> GetShopById(int shopId);
        Task<IEnumerable<Shop>> GetAllShops();
        Task<Shop> CreateShop(Shop newShop);
        Task<Shop> UpdateShop(Shop updatedShop);
        Task<bool> DeleteShop(int shopId);
    }
}
