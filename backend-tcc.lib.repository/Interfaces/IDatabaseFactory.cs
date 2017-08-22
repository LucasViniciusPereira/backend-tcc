using System;

namespace backend_tcc.lib.repository.Interfaces
{
    public interface IDatabaseFactory : IDisposable
    {
        IDatabaseContext GetContext();
    }
}
