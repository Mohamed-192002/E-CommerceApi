using ECommerce.Core.Interfaces;
using ECommerce.Infrastructure.Date;
using ECommerce.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure
{
    public static class InfrastructureRegistration
    {
        public static IServiceCollection InfrastructureConfigration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));
            services.AddScoped(typeof(ICategoryRepo), typeof(CategoryRepo));
            //services.AddScoped(typeof(IProductRepo), typeof(ProductRepo));
            //services.AddScoped(typeof(IProductRepo), typeof(ProductRepo));
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            var connection = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContext>(
                options => options.UseSqlServer(connection)
                );

            return services;
        }
    }
}
