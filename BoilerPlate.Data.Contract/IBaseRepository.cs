using BoilerPlate.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BoilerPlate.Data.Contract
{
    public interface IBaseRepository<T, TE> where T : BaseEntity<TE>
    {
        Task<T> GetByIdAsync(TE id);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity);
        Task<int> AddRangeAsync(IEnumerable<T> entities);
        Task UpdateAsync(T entity);
        Task UpdateRangeAsync(IEnumerable<T> entities);
        Task RemoveAsync(T entity);
        Task<int> CommitAsync();
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> predicate);
        Task<List<T>> GetAsync(Expression<Func<T, bool>> predicate, int count, int pageNumber);
        Task<int> CountAllAsync();
        Task<int> CountWhereAsync(Expression<Func<T, bool>> predicate);
    }
}
