using DataPlus.Contracts.Repositories;
using DataPlus.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataPlus.Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class, new()
    {
        protected RepositoryContext RepositoryContext { get; set; }

        public RepositoryBase(RepositoryContext repositoryContext)
        {
            this.RepositoryContext = repositoryContext;
        }

        public IQueryable<T> GetAll()
        {
            return this.RepositoryContext.Set<T>().AsNoTracking();
        }

        public IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression)
        {
            return this.RepositoryContext.Set<T>().Where(expression).AsNoTracking();
        }

        public T GetById(Guid Id)
        {
            var entity = this.RepositoryContext.Set<T>().Find(Id);
            if (entity != null)
                return entity;
            else
                return GetInstance();
        }

        private T GetInstance()
        {
            return new T();
        }

        public void Create(T entity)
        {
            this.RepositoryContext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            this.RepositoryContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            this.RepositoryContext.Set<T>().Remove(entity);
        }

        public void SaveChanges()
        {
            this.RepositoryContext.SaveChanges();
        }
    }
}
