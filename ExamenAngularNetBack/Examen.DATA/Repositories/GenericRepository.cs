using Examen.DATA.Repositories.interfaces;
using Examen.DATA.Repositories;

using Examen.ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Examen.DATA.Repositories
{
    public class GenericRepository<GenericModel> : IGenericRepository<GenericModel> where GenericModel : class
    {
        private readonly DbAngularNetContext _dbContext;

        public GenericRepository(DbAngularNetContext dbContext)
        {
            _dbContext=dbContext;
        }

        public async Task<IQueryable<GenericModel>> GetAll(Expression<Func<GenericModel, bool>> filtro = null)
        {
            try
            {
                IQueryable<GenericModel> queryModel = filtro == null ? _dbContext.Set<GenericModel>() : _dbContext.Set<GenericModel>().Where(filtro);
                return queryModel;


            }
            catch {
                throw;
            }
        }

        public async Task<GenericModel> Insert(GenericModel model)
        {
            try
            {
                _dbContext.Set<GenericModel>().Add(model);
                await _dbContext.SaveChangesAsync();
                return model;
            }
            catch
            {
                throw;
            }
        }

        public async Task<GenericModel> GetOne(Expression<Func<GenericModel, bool>> filtro)
        {
            try
            {
                GenericModel model = await _dbContext.Set<GenericModel>().FirstOrDefaultAsync(filtro);
                return model;
            }
            catch
            {
                throw;
            }
        }

        public async Task<GenericModel> Update(GenericModel model)
        {
            try
            {
                _dbContext.Set<GenericModel>().Update(model);
                await _dbContext.SaveChangesAsync();
                return model;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Delete(GenericModel model)
        {
            try
            {
                _dbContext.Set<GenericModel>().Remove(model);
                await _dbContext.SaveChangesAsync();
                return true;

            }
            catch
            {
                throw;
            }
        }


       

    }
}
