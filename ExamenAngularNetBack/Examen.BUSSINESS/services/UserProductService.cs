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
    public class UserProductService : IUserProductService
    {
        private readonly IGenericRepository<UserProduct> _UserProductRepository;
        public UserProductService(IGenericRepository<UserProduct> userProductRepository)
        {
            _UserProductRepository = userProductRepository;
        }
        public async Task<UserProduct> CreateUserProduct(UserProduct newUserProduct)
        {
            return await _UserProductRepository.Insert(newUserProduct);
        }

        public async Task<bool> DeleteUserProduct(int userProductId)
        {
            var userProductToDelete = await _UserProductRepository.GetOne(ps => ps.Id == userProductId);
            if (userProductToDelete == null)
                return false;

            return await _UserProductRepository.Delete(userProductToDelete);
        }

        public async Task<IEnumerable<UserProduct>> GetAllUserProduct()
        {
            var userProductsQuery = await _UserProductRepository.GetAll();
            var userProduct = userProductsQuery.Include(ps => ps.IdProductNavigation).Include(ps => ps.IdCustomerNavigation);
            return await userProduct.ToListAsync();
        }

        public async Task<UserProduct> GetUserProductById(int userProductId)
        {
            return await _UserProductRepository.GetOne(ps => ps.Id == userProductId);
        }

       

        public async Task<UserProduct> UpdateUserProduct(UserProduct updatedUserProduct)
        {
            return await _UserProductRepository.Update(updatedUserProduct);
        }
    }
}
