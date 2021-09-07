using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ABC.Repository.Common
{
    public interface IRepository<T, TContext> where T : class
    {
        T GetById(int id);
        IEnumerable<T> All();
        IEnumerable<T> Get(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetQuery(Expression<Func<T, bool>> predicate);

        int Count(Expression<Func<T, bool>> predicate);
        T Create(T entity);
        void Delete(Expression<Func<T, bool>> filter);
        void Delete(T entity);
        void Update(T entity);
    }
}
