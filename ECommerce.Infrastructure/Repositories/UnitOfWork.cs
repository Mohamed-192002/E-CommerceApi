using AutoMapper;
using Microsoft.Extensions.FileProviders;
using StackExchange.Redis;

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
        public IDeliveryMethodRepo DeliveryMethodRepo { get; }

        public IOrderRepo OrderRepo { get; }

        public UnitOfWork(AppDbContext context, IFileProvider fileProvider, IMapper mapper, IConnectionMultiplexer multiplexer)
        {
            _context = context;
            _fileProvider = fileProvider;
            _mapper = mapper;
            _multiplexer = multiplexer;
            CategoryRepo = new CategoryRepo(context);
            ProductRepo = new ProductRepo(context, fileProvider, mapper);
            BasketRepo = new BasketRepo(_multiplexer);
            DeliveryMethodRepo = new DeliveryMethodRepo(context);
            OrderRepo = new OrderRepo(context);

        }
    }
}
