using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Data.Repository.Base.Interfaces
{
    public interface IRepository : Abstract.IReadOnlyRepository
    {
        void Create<TEntity>(TEntity entity)
            where TEntity : class;

        void CreateList<TEntity>(IEnumerable<TEntity> entityList)
            where TEntity : class;

        void Update<TEntity>(TEntity entity, Expression<Func<TEntity, object[]>> excludeProperties = null, Expression<Func<TEntity, object[]>> includeProperties = null)
            where TEntity : class;

        void Delete<TEntity>(object id)
            where TEntity : class;

        void Delete<TEntity>(TEntity entity)
            where TEntity : class;

        void Save();

        Task SaveAsync();

        IEnumerable<TEntity> ResetDataFromDb<TEntity>(IEnumerable<TEntity> list, string entityPkName) where TEntity : class;
    }
}