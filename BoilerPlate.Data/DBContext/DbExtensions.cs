using BoilerPlate.Data.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Transactions;

namespace BoilerPlate.Data
{
    public static class DbExtensions
    {
        public static async Task<List<T>> ToListAsyncNoLock<T>(this IQueryable<T> query)
        {
            using (CreateNoLockTransaction())
            {
                return await query.ToListAsync();
            }
        }

        public static async Task<T> FirstAsyncNoLock<T>(this IQueryable<T> query)
        {
            using (CreateNoLockTransaction())
            {
                return await query.FirstAsync();
            }
        }

        public static async Task<T> FirstOrDefaultAsyncNoLock<T>(this IQueryable<T> query)
        {
            using (CreateNoLockTransaction())
            {
                return await query.FirstOrDefaultAsync();
            }
        }

        public static async Task<T> LastOrDefaultAsyncNoLock<T>(this IQueryable<T> query)
        {
            using (CreateNoLockTransaction())
            {
                return await query.LastOrDefaultAsync();
            }
        }

        public static async Task<T> SingleOrDefaultAsyncNoLock<T>(this IQueryable<T> query)
        {
            using (CreateNoLockTransaction())
            {
                return await query.SingleOrDefaultAsync();
            }
        }

        public static async Task<bool> AnyAsyncNoLock<T>(this IQueryable<T> query)
        {
            using (CreateNoLockTransaction())
            {
                return await query.AnyAsync();
            }
        }

        public static async Task<int> CountAsyncNoLock<T>(this IQueryable<T> query)
        {
            using (CreateNoLockTransaction())
            {
                return await query.CountAsync();
            }
        }

        public static async Task<int> CountAsyncNoLock<T>(this IQueryable<T> query, Expression<Func<T, bool>> predicate)
        {
            using (CreateNoLockTransaction())
            {
                return await query.CountAsync(predicate);
            }
        }

        public static async Task<T> FirstOrDefaultAsyncNoLock<T>(this IQueryable<T> query, Expression<Func<T, bool>> predicate)
        {
            using (CreateNoLockTransaction())
            {
                return await query.FirstOrDefaultAsync(predicate);
            }

        }

        public static async Task<T[]> ToArrayAsyncNoLock<T>(this IQueryable<T> query)
        {
            using (CreateNoLockTransaction())
            {
                return await query.ToArrayAsync();
            }
        }

        public static TransactionScope CreateNoLockTransaction()
        {
            return new TransactionScope(
                                     TransactionScopeOption.Required,
                                     new TransactionOptions
                                     {
                                         IsolationLevel = IsolationLevel.ReadUncommitted,
                                     }, TransactionScopeAsyncFlowOption.Enabled);
        }
    }
}
