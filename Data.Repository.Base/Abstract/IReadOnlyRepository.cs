using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Data.Repository.Base.Abstract
{
    public interface IReadOnlyRepository
    {
        IEnumerable<TEntity> Get<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            IEnumerable<Expression<Func<TEntity, object>>> includeProperties = null,
            int? skip = null,
            int? take = null,
            bool isDistinct = false)
            where TEntity : class;

        Task<IEnumerable<TEntity>> GetAsync<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            IEnumerable<Expression<Func<TEntity, object>>> includeProperties = null,
            int? skip = null,
            int? take = null,
            bool isDistinct = false)
            where TEntity : class;

        IEnumerable<TDto> Get<TEntity, TDto>(
            Expression<Func<TEntity, TDto>> selectProperties,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            IEnumerable<Expression<Func<TEntity, object>>> includeProperties = null,
            int? skip = null,
            int? take = null,
            bool isDistinct = false)
            where TEntity : class
            where TDto : class;

        Task<IEnumerable<TDto>> GetAsync<TEntity, TDto>(
            Expression<Func<TEntity, TDto>> selectProperties,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            IEnumerable<Expression<Func<TEntity, object>>> includeProperties = null,
            int? skip = null,
            int? take = null,
            bool isDistinct = false)
            where TEntity : class
            where TDto : class;

        TEntity GetById<TEntity>(object id)
            where TEntity : class;

        Task<TEntity> GetByIdAsync<TEntity>(object id)
            where TEntity : class;

        int GetCount<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class;

        Task<int> GetCountAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class;

        bool GetExists<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class;

        Task<bool> GetExistsAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class;
    }
}
