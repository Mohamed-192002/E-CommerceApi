using ECommerce.Core.Interfaces;
using ECommerce.Infrastructure.Date;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public ICategoryRepo CategoryRepo { get; }
        public IProductRepo ProductRepo { get; }
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            CategoryRepo = new CategoryRepo(context);
            ProductRepo = new ProductRepo(context);
        }
    }
}
