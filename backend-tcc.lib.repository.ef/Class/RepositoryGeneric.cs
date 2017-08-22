using backend_tcc.lib.repository.Interfaces;

namespace backend_tcc.lib.repository.ef.Class
{
    public class RepositoryGeneric<T> : RepositoryBase<T>, IRepository<T> where T : class
    {
        public RepositoryGeneric(IDatabaseContext _db)
            : base(_db)
        {

        }
    }
}
