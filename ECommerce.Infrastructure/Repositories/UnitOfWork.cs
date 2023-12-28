using AutoMapper;
using ECommerce.Core.Interfaces;
using ECommerce.Infrastructure.Date;
using Microsoft.Extensions.FileProviders;
using StackExchange.Redis;
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
        private readonly IFileProvider _fileProvider;
        private readonly IMapper _mapper;
        private readonly IConnectionMultiplexer _multiplexer;

        public ICategoryRepo CategoryRepo { get; }
        public IProductRepo ProductRepo { get; }
        public IBasketRepo BasketRepo { get; }

        public UnitOfWork(AppDbContext context, IFileProvider fileProvider, IMapper mapper, IConnectionMultiplexer multiplexer)
        {
            _context = context;
            _fileProvider = fileProvider;
            _mapper = mapper;
            _multiplexer = multiplexer;
            CategoryRepo = new CategoryRepo(context);
            ProductRepo = new ProductRepo(context, fileProvider, mapper);
            BasketRepo = new BasketRepo(_multiplexer);
        }
    }
}
