using ECommerce.Core.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;

namespace ECommerce.Infrastructure.Repositories
{
    public class OrderRepo : GenericRepo<Order>, IOrderRepo
    {
        private readonly AppDbContext _context;
        public OrderRepo(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Order>> GetAllAsync(string[] includes = null
           , Expression<Func<Order, object>> orderBy = null, string orderByDirection = "ASC")
        {
            IQueryable<Order> query = _context.Set<Order>();

            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);

            if (orderBy != null)
            {
                if (orderByDirection == "ASC")
                    query = query.OrderBy(orderBy);
                else
                    query = query.OrderByDescending(orderBy);
            }

            return query;
        }
    }
}
