namespace CloudEngineMX.Erp.SetinTred.Core.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Common;
    using Entities;
    using Microsoft.EntityFrameworkCore.Query;

    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> PrepareQuery(
           IQueryable<T> query,
           Expression<Func<T, bool>> predicate = null,
           Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
           int? take = null
       );

        Task<decimal?> SumAsync(
            Expression<Func<T, bool>> predicate = null
        );

        Task<int> CountAsync(
            Expression<Func<T, bool>> predicate = null
        );

        Task<IEnumerable<T>> GetAllAsync(
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            int? take = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null
        );

        Task<DataCollection<T>> GetPagedAsync(
            int page,
            int take,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null
        );

        Task<T> FirstAsync(
            Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null
        );

        Task<T> FirstOrDefaultAsync(
            Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null
        );

        Task<T> SingleAsync(
            Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null
        );

        Task<T> SingleOrDefaultAsync(
            Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null
        );

        Task<T> AddAsync(T t);

        Task AddAsync(IEnumerable<T> t);

        void Remove(T t);

        void Remove(IEnumerable<T> t);

        void Update(T t);

        void Update(IEnumerable<T> t);

        Task<IEnumerable<T>> ExecuteStoreProcedureAsync(string commandExec);


    }
}
