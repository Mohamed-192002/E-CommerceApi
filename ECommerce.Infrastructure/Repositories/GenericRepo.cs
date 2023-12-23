using ECommerce.Core.Interfaces;
using ECommerce.Infrastructure.Date;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Repositories
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        private readonly AppDbContext _context;

        public GenericRepo(AppDbContext context) => _context = context;

        public async Task<IEnumerable<T>> GetAllAsync(string[] includes = null, Expression<Func<T, object>> orderBy = null, string orderByDirection = "ASC")
        {
            IQueryable<T> query = _context.Set<T>();

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

            return await query.ToListAsync();
        }
        public async Task<T> GetAsync(Expression<Func<T, bool>> criteria, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null)
                foreach (var incluse in includes)
                    query = query.Include(incluse);

            return await query.SingleOrDefaultAsync(criteria);
        }
        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public async Task Update(int id, T value)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity is not null)
            {
                _context.Update(entity);
                await _context.SaveChangesAsync();
            }
        }
        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        => await _context.Set<T>().AsNoTracking().ToListAsync();

        //public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        //{
        //    var quary = _context.Set<T>().AsQueryable();
        //    foreach (var item in includes)
        //        quary = quary.Include(item);
        //    return await quary.ToListAsync();
        //}

        //public async Task<T> GetAsync(int id)
        //=> await _context.Set<T>().FindAsync(id);

        //public async Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
        //{
        //    IQueryable<T> quary = _context.Set<T>();
        //    foreach (var item in includes)
        //        quary = quary.Include(item);
        //    return await ((DbSet<T>)quary).FindAsync(id);
        //}
    }
}
