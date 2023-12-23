using AutoMapper;
using ECommerce.Core.Interfaces;
using ECommerce.Infrastructure.Date;
using Microsoft.Extensions.FileProviders;
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
        private readonly IFileProvider fileProvider;
        private readonly IMapper mapper;
        public ICategoryRepo CategoryRepo { get; }
        public IProductRepo ProductRepo { get; }
        public UnitOfWork(AppDbContext context, IFileProvider fileProvider, IMapper mapper)
        {
            _context = context;
            this.fileProvider = fileProvider;
            this.mapper = mapper;
            CategoryRepo = new CategoryRepo(context);
            ProductRepo = new ProductRepo(context, fileProvider, mapper);
        }
    }
}
