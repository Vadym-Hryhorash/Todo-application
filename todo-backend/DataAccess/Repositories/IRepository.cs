using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace TodoApp.DataAccess.Repositories
{
    public interface IRepository<T>
    {
        public void Add(T entity);
        public void Update(T entity);
        public void Delete(T entity);
        public T? GetById(int id);
        public IQueryable<T> GetAll();
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        public void SaveChanges();
    }
}
