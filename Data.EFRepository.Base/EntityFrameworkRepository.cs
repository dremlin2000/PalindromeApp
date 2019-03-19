using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Data.Repository.Base.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.EFRepository.Base
{
    public class EntityFrameworkRepository<TContext> : EntityFrameworkReadOnlyRepository<TContext>, IRepository
    where TContext : DbContext
    {
        public EntityFrameworkRepository(TContext context)
            : base(context)
        {
        }

        public virtual void Create<TEntity>(TEntity entity)
            where TEntity : class
        {
            context.Set<TEntity>().Add(entity);
        }

        public virtual void CreateList<TEntity>(IEnumerable<TEntity> entityList)
            where TEntity : class
        {
            context.Set<TEntity>().AddRange(entityList);
        }
        
        public virtual void Update<TEntity>(TEntity entity, Expression<Func<TEntity, object[]>> excludeProperties = null,
            Expression<Func<TEntity, object[]>> includeProperties = null)
            where TEntity : class
        {
            //Check if the local entity exists and is attached
            if (context.Set<TEntity>().Local.Any(x => x == entity))
            {
                context.Set<TEntity>().Update(entity);
            }
            else
            {
                context.Set<TEntity>().Attach(entity);
                context.Entry(entity).State = includeProperties != null ? EntityState.Unchanged : EntityState.Modified;
            }

            //Exclude fileds from update SQL statement
            StringCollection strCollection = new StringCollection();
            NewArrayExpression array = null;

            if (excludeProperties != null)
                array = excludeProperties.Body as NewArrayExpression;
            else
                if (includeProperties != null)
                    array = includeProperties?.Body as NewArrayExpression;
            if (array != null)
                foreach (var expression in array.Expressions)
                {
                    string propertyName;
                    if (expression is UnaryExpression)
                    {
                        propertyName = ((MemberExpression)((UnaryExpression) expression).Operand).Member.Name;
                    }
                    else
                    {
                        propertyName = ((MemberExpression)expression).Member.Name;
                    }

                    strCollection.Add(propertyName);
                }

            if (excludeProperties != null)
            {
                foreach (var propName in strCollection)
                    context.Entry(entity).Property(propName).IsModified = false;
            }
            else if (includeProperties != null)
            {
                foreach (var propName in strCollection)
                    context.Entry(entity).Property(propName).IsModified = true;
            }
        }

        public virtual void Delete<TEntity>(object id)
            where TEntity : class
        {
            TEntity entity = context.Set<TEntity>().Find(id);
            Delete(entity);
        }

        public virtual void Delete<TEntity>(TEntity entity)
            where TEntity : class
        {
            var dbSet = context.Set<TEntity>();
            //Check if the local entity exists and is attached
            if (!dbSet.Local.Any(x => x == entity)) dbSet.Attach(entity);
            dbSet.Remove(entity);
        }

        //public virtual void BeginTransaction()
        //{
        //    if (context.Database.CurrentTransaction == null)
        //        _dbContextTransaction = context.Database.BeginTransaction();
        //}

        //public virtual void CommitTransaction()
        //{
        //    _dbContextTransaction?.Commit();
        //}

        //public virtual void RollbackTransaction()
        //{
        //    if (_dbContextTransaction?.UnderlyingTransaction?.Connection != null)
        //        _dbContextTransaction.Rollback();
        //}

        //public virtual void Save()
        //{
        //    try
        //    {
        //        context.SaveChanges();
        //    }
        //    catch (DbEntityValidationException e)
        //    {
        //        ThrowEnhancedValidationException(e);
        //    }
        //}

        //public virtual Task SaveAsync()
        //{
        //    try
        //    {
        //        return context.SaveChangesAsync();
        //    }
        //    catch (DbEntityValidationException e)
        //    {
        //        ThrowEnhancedValidationException(e);
        //    }

        //    return Task.FromResult(0);
        //}

        public virtual void Save()
        {
            context.SaveChanges();
        }

        public virtual async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }
        
        //protected virtual void ThrowEnhancedValidationException(DbEntityValidationException e)
        //{
        //    var errorMessages = e.EntityValidationErrors
        //            .SelectMany(x => x.ValidationErrors)
        //            .Select(x => x.ErrorMessage);

        //    var fullErrorMessage = string.Join("; ", errorMessages);
        //    var exceptionMessage = string.Concat(e.Message, " The validation errors are: ", fullErrorMessage);
        //    throw new DbEntityValidationException(exceptionMessage, e.EntityValidationErrors);
        //}


        // Purpose of this generic method is: sometimes we want to save a new object record which has 
        // many-to-many relationship with other entity. In order to make EF not save-new-record for those existing entities, 
        // we should reset them directly from database
        public virtual IEnumerable<TEntity> ResetDataFromDb<TEntity>(IEnumerable<TEntity> list, string entityPkName) where TEntity : class
        {
            return list.Select(obj => typeof(TEntity).GetProperty(entityPkName).GetValue(obj))
                       .Select(value => context.Set<TEntity>().Find(value)).ToList();
        }
    }
}
