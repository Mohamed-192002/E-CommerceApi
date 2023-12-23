using ECommerce.Core.DTO;
using ECommerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Interfaces
{
    public interface IProductRepo:IGenericRepo<Product>
    {
        Task<bool> AddAsync(CreateProductDTO productDTO);
        Task<bool> UpdateAsync(UpdateProductDTO model);
    }
}
