using Examen.BUSSINESS.services;
using Examen.BUSSINESS.services.interfaces;
using Examen.DATA.Repositories;
using Examen.DATA.Repositories.interfaces;
using Examen.ENTITY;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.BUSSINESS.Dependencies
{
    public static class Dependency
    {
        public static void InyecDependencies(this IServiceCollection services, IConfiguration configuration ) {
            services.AddDbContext<DbAngularNetContext>(options => {
                options.UseSqlServer(configuration.GetConnectionString("cadenaSQL"));
            });

            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IShopSevice, ShopService>();
            services.AddScoped<IProductShopService, ProductShopService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserProductService, UserProductService>();





        }

    }
}
