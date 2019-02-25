using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Vidhalla.Core.Repositories;

namespace Vidhalla.Persistence
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly DbContext GenericContext;

        public GenericRepository(DbContext concreteContext)
        {
            GenericContext = concreteContext;
        }

        //====================================================================//

        public virtual T Get(int id)
        {
            return GenericContext.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return GenericContext.Set<T>().ToList();
        }

        public T Get(Expression<Func<T, bool>> predicate)
        {
            return GenericContext.Set<T>().SingleOrDefault(predicate);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            return GenericContext.Set<T>().Where(predicate).ToList();
        }

        public void Add(T entity)
        {
            GenericContext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            GenericContext.Set<T>().Remove(entity);
        }

    }
}