using ECommerce.Core.Entities;
using ECommerce.Core.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Interfaces
{
    public interface IOrderRepo : IGenericRepo<Order>
    {
        Task<IEnumerable<Order>> GetAllAsync(string[] includes = null
           , Expression<Func<Order, object>> orderBy = null, string orderByDirection = "ASC");
    }
}
