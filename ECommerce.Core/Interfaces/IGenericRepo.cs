using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Interfaces
{
    public interface IGenericRepo<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        //Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
        //Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> GetAllAsync(string[] includes = null, Expression<Func<T, object>> orderBy = null, string orderByDirection = "ASC");
        Task<T> GetAsync(Expression<Func<T, bool>> criteria, string[] includes = null);
        //Task<T> GetAsync(int id);
        Task AddAsync(T entity);
        Task Update(int id, T value);
        Task DeleteAsync(int id);

    }
}
