using ECommerce.Core.DTO;
using ECommerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Interfaces
{
    public interface IProductRepo : IGenericRepo<Product>
    {
        Task<IEnumerable<Product>> GetAllAsync(int? pageNumber, int? pageSize
            , Expression<Func<Product, bool>> SearchByCategoryId,string SearchByName
            , string[] includes = null
            , Expression<Func<Product, object>> orderBy = null, string orderByDirection = "ASC");

        Task<bool> AddAsync(CreateProductDTO productDTO);
        Task<bool> UpdateAsync(UpdateProductDTO model);
    }
}
