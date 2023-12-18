using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Interfaces
{
    public interface IUnitOfWork
    {
        public ICategoryRepo CategoryRepo { get; }
        public IProductRepo ProductRepo { get; }

    }
}
