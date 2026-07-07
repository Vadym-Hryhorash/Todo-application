using TodoApp.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace TodoApp.DataAccess.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContextTodoApp _db;
        private readonly DbSet<T> _dbSet;

        public Repository(DbContextTodoApp context)
        {
            _db = context;
            _dbSet = _db.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public T? GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet;
        }
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression);
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}
