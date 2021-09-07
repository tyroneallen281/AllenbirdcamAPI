using ABC.Data.DatabaseContext;
using ABC.Domain.Common;
using ABC.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ABC.Repository.Common
{
    public class RepositoryBase<T, TContext> : IRepository<T, TContext> where T : class
        where TContext : ABCDbContext
    {
        private readonly ABCDbContext _dbContext;

        public RepositoryBase(ABCDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual T GetById(int id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public virtual IEnumerable<T> All()
        {
            return Get(x => true);
        }

        public virtual IEnumerable<T> Get(Expression<Func<T, bool>> predicate)
        {
            var resetSet = _dbContext.Set<T>()
                   .Where(predicate)
                   .AsQueryable();
            if (typeof(T).GetInterfaces().Contains(typeof(ILogicalDelete)))
            {
                resetSet = resetSet.Where(x => !((ILogicalDelete)x).IsDeleted);
            }
            
            return resetSet;
        }

        public virtual IQueryable<T> GetQuery(Expression<Func<T, bool>> predicate)
        {
            var resetSet = _dbContext.Set<T>()
                   .Where(predicate)
                   .AsQueryable();
            if (typeof(T).GetInterfaces().Contains(typeof(ILogicalDelete)))
            {
                resetSet = resetSet.Where(x => !((ILogicalDelete)x).IsDeleted);
            }

            return resetSet;

        }
        public virtual int Count(Expression<Func<T, bool>> predicate)
        {
            var resetSet = _dbContext.Set<T>()
                   .Where(predicate)
                   .AsQueryable();
            if (typeof(T).GetInterfaces().Contains(typeof(ILogicalDelete)))
            {
                resetSet = resetSet.Where(x => !((ILogicalDelete)x).IsDeleted);
            }
        
            return resetSet.Count();
        }
       
        public T Create(T entity)
        {
            if (typeof(T).GetInterfaces().Contains(typeof(IAuditableEntity)))
            {
                ((IAuditableEntity)entity).DateCreated = DateTime.Now;
                ((IAuditableEntity)entity).DateModified = DateTime.Now;
            }
            var result = _dbContext.Set<T>().Add(entity);
            _dbContext.SaveChanges();
            return result?.Entity;
        }

        public void Update(T entity)
        {

            if (typeof(T).GetInterfaces().Contains(typeof(IAuditableEntity)))
            {
                ((IAuditableEntity)entity).DateModified = DateTime.Now;
            }
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Delete(Expression<Func<T, bool>> filter)
        {
            var resultSet = _dbContext.Set<T>().Where<T>(filter).ToList();
            foreach (var entity in resultSet)
            {
                if (typeof(T).GetProperties().Any(p => p.Name.ToLower() == "isdeleted"))
                {
                    ((ILogicalDelete)entity).IsDeleted = true;
                    _dbContext.Entry(entity).State = EntityState.Modified;

                }
                else
                {
                    _dbContext.Set<T>().Remove(entity);

                }
            }
            _dbContext.SaveChanges();
        }

        public void Delete(T entity)
        {
            if (typeof(T).GetProperties().Any(p => p.Name.ToLower() == "isdeleted"))
            {
                ((ILogicalDelete)entity).IsDeleted = true;
                _dbContext.Entry(entity).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            else
            {
                _dbContext.Set<T>().Remove(entity);
                _dbContext.SaveChanges();
            }
        }

        
    }
}