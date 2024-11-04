using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DataContext _Context;

        private DbSet<T> _entities;
        public Repository()
        {
            _Context = new DataContext();
            _entities = _Context.Set<T>();
        }
        public Repository(DataContext context)
        {
            _Context = context;
            _entities = context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return _entities.ToList();
        }

        public T GetById(object id)
        {
            return _entities.Find(id);
        }

        public void Save()
        {
            _Context.SaveChanges();
        }

        public T Insert(T obj)
        {
            _entities.Add(obj);
            _Context.SaveChanges();
            return obj;
        }

        public void Update(T obj)
        {
            _entities.Update(obj);
        }
        public void Delete(object id)
        {
            T entity = _entities.Find(id);
            _entities.Remove(entity);
        }
    }
}
