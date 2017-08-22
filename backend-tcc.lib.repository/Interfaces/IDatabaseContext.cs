using System;

namespace backend_tcc.lib.repository.Interfaces
{
    public interface IDatabaseContext : IDisposable
    {
        IRepository<T> GetRepository<T>() where T : class;

        int Commit(string UserID);

        void Rollback();
    }
}
