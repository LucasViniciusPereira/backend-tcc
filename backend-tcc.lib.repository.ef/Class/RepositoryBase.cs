using backend_tcc.lib.repository.Class;
using backend_tcc.lib.repository.Interfaces;
using System.Data.Entity;
using System.Linq;

namespace backend_tcc.lib.repository.ef.Class
{
    public abstract class RepositoryBase<T> : Disposable, IRepository<T> where T : class
    {
        protected readonly DbContext db;

        public RepositoryBase(IDatabaseContext dbContext)
        {
            db = dbContext as DbContext;
        }

        public IQueryable<T> GetQuery(bool TrackingChanges = true)
        {
            IQueryable<T> queryable = null;

            if (TrackingChanges)
                queryable = db.Set<T>().AsQueryable();
            else
                queryable = db.Set<T>().AsNoTracking().AsQueryable();

            return queryable;
        }

        public T Find(params object[] key)
        {
            return db.Set<T>().Find(key);
        }
        public void Update(T obj)
        {
            db.Entry(obj).State = EntityState.Modified;
        }
        public int SaveChanges()
        {
            return db.SaveChanges();
        }
        public void Insert(T obj)
        {
            db.Set<T>().Add(obj);
        }
        public void Delete(T obj)
        {
            db.Set<T>().Remove(obj);
        }                
    }
}
