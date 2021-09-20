namespace CloudEngineMX.Erp.SetinTred.Infrastructure.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Core.Common;
    using Core.Entities;
    using Core.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Query;

    public class EfRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly DataContext _dataContext;
        private readonly DbSet<T> _dbSet;

        public EfRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
            _dbSet = _dataContext.Set<T>();
        }

        public IQueryable<T> PrepareQuery(
            IQueryable<T> query,
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            int? take = null)
        {
            if (include != null)
                query = include(query);

            if (predicate != null)
                query = query.Where(predicate);

            if (orderBy != null)
                query = orderBy(query);

            if (take.HasValue)
                query = query.Take(Convert.ToInt32(take));

            return query;
        }

        public async Task<decimal?> SumAsync(Expression<Func<T, bool>> predicate = null)
        {
            var query = _dataContext.Set<T>().AsQueryable();

            query = PrepareQuery(query, predicate);

            return await ((IQueryable<decimal?>)query).SumAsync();
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate = null)
        {
            var query = _dataContext.Set<T>().AsQueryable();

            query = PrepareQuery(query, predicate);

            return await query.CountAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            int? take = null,
            Func<IQueryable<T>,
                IIncludableQueryable<T, object>> include = null)
        {
            var query = _dataContext.Set<T>().AsQueryable();

            query = PrepareQuery(query, predicate, include, orderBy, take);

            return await query.ToListAsync();
        }

        public async Task<DataCollection<T>> GetPagedAsync(
            int page,
            int take,
            Func<IQueryable<T>,
                IOrderedQueryable<T>> orderBy,
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>,
                IIncludableQueryable<T, object>> include = null)
        {
            var query = _dataContext.Set<T>().AsQueryable();
            var originalPages = page;

            page--;

            if (page > 0)
                page = page * take;

            query = PrepareQuery(query, predicate, include, orderBy);

            var result = new DataCollection<T>
            {
                Items = await query.Skip(page).Take(take).ToListAsync(),
                Total = await query.CountAsync(),
                Page = originalPages
            };

            if (result.Total > 0)
            {
                result.Pages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(result.Total) / take));
            }

            return result;
        }

        public async Task<T> FirstAsync(
            Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            var query = _dataContext.Set<T>().AsQueryable();

            query = PrepareQuery(query, predicate, include, orderBy);

            return await query.FirstAsync();
        }

        public async Task<T> FirstOrDefaultAsync(
            Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            var query = _dataContext.Set<T>().AsQueryable();

            query = PrepareQuery(query, predicate, include, orderBy);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<T> SingleAsync(
            Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            var query = _dataContext.Set<T>().AsQueryable();

            query = PrepareQuery(query, predicate, include);

            return await query.SingleAsync();
        }

        public async Task<T> SingleOrDefaultAsync(
            Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            var query = _dataContext.Set<T>().AsQueryable();

            query = PrepareQuery(query, predicate, include);

            return await query.SingleOrDefaultAsync();
        }

        public virtual async Task<T> AddAsync(T t)
        {
            await _dataContext.AddAsync(t);

            return t;
        }

        public async Task AddAsync(IEnumerable<T> t)
        {
            await _dataContext.AddRangeAsync(t);
        }

        public void Remove(T t)
        {
            _dataContext.Remove(t);
        }

        public void Remove(IEnumerable<T> t)
        {
            _dataContext.RemoveRange(t);
            _dataContext.SaveChanges();
        }

        public virtual void Update(T t)
        {
            _dataContext.Update(t);
            //_dataContext.SaveChanges();
        }

        public void Update(IEnumerable<T> t)
        {
            _dataContext.UpdateRange(t);
            //_dataContext.SaveChanges();
        }

        public async Task<IEnumerable<T>> ExecuteStoreProcedureAsync(string commandExec)
        {
            _dataContext.Database.SetCommandTimeout(300);
            var result = await _dbSet.FromSqlRaw(commandExec).ToListAsync();
            return result;
        }
    }
}
