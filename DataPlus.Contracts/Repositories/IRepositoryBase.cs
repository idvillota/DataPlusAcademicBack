using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataPlus.Contracts.Repositories
{
    public interface IRepositoryBase<T> where T : class
    {
        IQueryable<T> GetAll();
        T GetById(Guid Id);
        IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        void SaveChanges();
    }
}
