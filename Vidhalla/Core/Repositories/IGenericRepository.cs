﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Vidhalla.Core.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        T Get(int id);
        IEnumerable<T> GetAll();

        T Get(Expression<Func<T, bool>> predicate);
        IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate);

        void Add(T entity);
        void Delete(T entity);
    }
}
