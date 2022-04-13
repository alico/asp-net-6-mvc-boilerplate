using BoilerPlate.Data.Contract;
using BoilerPlate.Data.Entities;
using BoilerPlate.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BoilerPlate.Data
{
    public class BaseRepository<T, TE> : IBaseRepository<T, TE> where T : BaseEntity<TE> 
    {
        protected readonly ApplicationDataContext _dataContext;
        protected readonly ConnectionStrings _connectionStrings;
      

        public BaseRepository(ConnectionStrings connectionStrings, IDataContext dataContext)
        {
            _connectionStrings = connectionStrings;
            _dataContext = (ApplicationDataContext)dataContext;
        }

        public virtual async Task<T> GetByIdAsync(TE id)
        {
            return await _dataContext.Set<T>().FindAsync(id);
        }

        public virtual Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return _dataContext.Set<T>().FirstOrDefaultAsyncNoLock(predicate);
        }

        public virtual async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dataContext.Set<T>().AnyAsync(predicate);
        }

        public virtual async Task AddAsync(T entity)
        {
            await _dataContext.Set<T>().AddAsync(entity);
        }

        public virtual async Task<int> AddRangeAsync(IEnumerable<T> entities)
        {
            await _dataContext.AddRangeAsync(entities.ToList());
            return entities.Count();
        }

        public virtual async Task UpdateAsync(T entity)
        {
            _dataContext.Set<T>().Update(entity);
        }

        public virtual async Task UpdateRangeAsync(IEnumerable<T> entities)
        {
            _dataContext.Set<T>().UpdateRange(entities);
        }

        public virtual async Task<int> CommitAsync()
        {
            return await _dataContext.SaveChangesAsync();
        }

        public virtual Task RemoveAsync(T entity)
        {
            _dataContext.Set<T>().Remove(entity);
            return _dataContext.SaveChangesAsync();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dataContext.Set<T>().ToListAsyncNoLock();
        }

        public async Task<List<T>> GetAsync(Expression<Func<T, bool>> predicate, int count, int pageNumber)
        {
            return await _dataContext.Set<T>().Where(predicate).Skip(count * (pageNumber - 1)).Take(count).ToListAsyncNoLock();
        }

        public virtual async Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> predicate)
        {
            var result = await _dataContext.Set<T>().Where(predicate).ToListAsyncNoLock();
            return result;
        }

        public virtual async Task<int> CountAllAsync()
        {
            return await _dataContext.Set<T>().CountAsyncNoLock();
        }


        public virtual Task<int> CountWhereAsync(Expression<Func<T, bool>> predicate)
        {
            return _dataContext.Set<T>().CountAsyncNoLock(predicate);
        }


    }
}
