using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Examen.DATA.Repositories.interfaces
{
    public interface IGenericRepository<GenericModel> where GenericModel : class
    {
        Task<GenericModel> GetOne(Expression<Func<GenericModel, bool>> filtro);
        Task<GenericModel> Insert(GenericModel model);

        Task<GenericModel> Update(GenericModel model);
        Task<bool> Delete(GenericModel model);

        Task<IQueryable<GenericModel>> GetAll(Expression<Func<GenericModel, bool>> filtro = null);




    }
}
