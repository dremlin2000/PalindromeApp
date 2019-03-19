using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Data.EFRepository.Base
{
    public class EntityFrameworkReadOnlyRepository<TContext> : Data.Repository.Base.Abstract.IReadOnlyRepository, IDisposable
    where TContext : DbContext
    {
        protected readonly TContext context;

        public EntityFrameworkReadOnlyRepository(TContext context)
        {
            this.context = context;

            //context.Database.Log = s => Debug.WriteLine(s);
        }

        //private IEnumerable<EntityKeyMember> GetKeys<TEntity>(TEntity entity) where TEntity : class
        //{
        //    var entityKeyMemberArr = new List<EntityKeyMember>();

        //    var objectSet = ((IObjectContextAdapter)context).ObjectContext.CreateObjectSet<TEntity>();
        //    var keyNames = objectSet.EntitySet.ElementType.KeyMembers.Select(k => k.Name);

        //    Type type = typeof(TEntity);
        //    foreach (var keyName in keyNames)
        //    {
        //        entityKeyMemberArr.Add(new EntityKeyMember(keyName, type.GetProperty(keyName).GetValue(entity, null)));
        //    }

        //    return entityKeyMemberArr;
        //}

        //protected EntityKey GetEntityKey<TEntity>(TEntity entity) where TEntity : class
        //{
        //    var objectContext = ((IObjectContextAdapter)context).ObjectContext;
        //    var keys = GetKeys(entity);

        //    var container = objectContext.MetadataWorkspace.GetEntityContainer(objectContext.DefaultContainerName, DataSpace.CSpace);
        //    string setName = (from meta in container.BaseEntitySets
        //                      where meta.ElementType.Name == typeof(TEntity).Name
        //                      select meta.Name).First();

        //    return new EntityKey
        //    {
        //        EntityContainerName = container.Name,
        //        EntitySetName = setName,
        //        EntityKeyValues = keys.ToArray()
        //    };
        //}

        //protected ObjectStateEntry GetEntityState<TEntity>(TEntity entity) where TEntity : class
        //{
        //    //EntityKey
        //    var objectContext = ((IObjectContextAdapter)context).ObjectContext;
        //    ObjectStateEntry entry;
        //    objectContext.ObjectStateManager.TryGetObjectStateEntry(GetEntityKey(entity), out entry);

        //    return entry;
        //}

        //protected ObjectStateEntry GetEntityState<TEntity>(TEntity entity) where TEntity : class
        //{
        //    //EntityKey
        //    context.ChangeTracker.Entries().Where(x => x.Entity)
        //    var objectContext = ((IObjectContextAdapter)context).ObjectContext;
        //    ObjectStateEntry entry;
        //    objectContext.ObjectStateManager.TryGetObjectStateEntry(GetEntityKey(entity), out entry);

        //    return entry;
        //}

        protected virtual IQueryable<TEntity> GetQueryable<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            IEnumerable<Expression<Func<TEntity, object>>> includeProperties = null,
            int? skip = null,
            int? take = null,
            bool isDistinct = false)
            where TEntity : class
        {
            var query = context.Set<TEntity>().AsNoTracking();

            if (isDistinct)
            {
                query = query.Distinct();
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query;
        }


        public virtual IEnumerable<TEntity> Get<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            IEnumerable<Expression<Func<TEntity, object>>> includeProperties = null,
            int? skip = null,
            int? take = null,
            bool isDistinct = false)
            where TEntity : class
        {
            return GetQueryable(filter, orderBy, includeProperties, skip, take, isDistinct).ToList();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            IEnumerable<Expression<Func<TEntity, object>>> includeProperties = null,
            int? skip = null,
            int? take = null,
            bool isDistinct = false)
            where TEntity : class
        {
            return await GetQueryable(filter, orderBy, includeProperties, skip, take, isDistinct).ToListAsync();
        }

        public virtual IEnumerable<TDto> Get<TEntity, TDto>(
            Expression<Func<TEntity, TDto>> selectProperties,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            IEnumerable<Expression<Func<TEntity, object>>> includeProperties = null,
            int? skip = null,
            int? take = null,
            bool isDistinct = false)
            where TEntity : class
            where TDto : class
        {
            return GetQueryable(filter, orderBy, includeProperties, skip, take, isDistinct).Select(selectProperties).ToList();
        }

        public virtual async Task<IEnumerable<TDto>> GetAsync<TEntity, TDto>(
            Expression<Func<TEntity, TDto>> selectProperties,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            IEnumerable<Expression<Func<TEntity, object>>> includeProperties = null,
            int? skip = null,
            int? take = null,
            bool isDistinct = false)
            where TEntity : class
            where TDto : class
        {
            return await GetQueryable(filter, orderBy, includeProperties, skip, take, isDistinct).Select(selectProperties).ToListAsync();
        }

        public virtual TEntity GetById<TEntity>(object id)
            where TEntity : class
        {
            return context.Set<TEntity>().Find(id);
        }

        public virtual Task<TEntity> GetByIdAsync<TEntity>(object id)
            where TEntity : class
        {
            return context.Set<TEntity>().FindAsync(id);
        }

        public virtual int GetCount<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class
        {
            return GetQueryable(filter).Count();
        }

        public virtual Task<int> GetCountAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class
        {
            return GetQueryable(filter).CountAsync();
        }

        public virtual bool GetExists<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class
        {
            return GetQueryable(filter).Any();
        }

        public virtual Task<bool> GetExistsAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class
        {
            return GetQueryable(filter).AnyAsync();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    context.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~EntityFrameworkReadOnlyRepository() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
