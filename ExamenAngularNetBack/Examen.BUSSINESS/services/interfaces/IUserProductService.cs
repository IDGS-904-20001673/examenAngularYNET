using Examen.ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.BUSSINESS.services.interfaces
{
    public interface IUserProductService
    {
        Task<UserProduct> GetUserProductById(int userProductId);
        Task<IEnumerable<UserProduct>> GetAllUserProduct();
        Task<UserProduct> CreateUserProduct(UserProduct newUserProduct);
        Task<UserProduct> UpdateUserProduct(UserProduct updatedUserProduct);
        Task<bool> DeleteUserProduct(int userProductId);
    }
}
