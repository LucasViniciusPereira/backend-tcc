using System.Linq;

namespace backend_tcc.lib.repository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetQuery(bool TrackingChanges = true);

        T Find(params object[] key);

        void Update(T obj);

        int SaveChanges();

        void Insert(T obj);

        void Delete(T obj);
        
    }
}
