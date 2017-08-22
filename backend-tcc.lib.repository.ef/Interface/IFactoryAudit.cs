using backend_tcc.lib.repository.ef.Class;
using System.Data.Entity;

namespace backend_tcc.lib.repository.ef.Interface
{
    public interface IFactoryAudit
    {
        IAuditorEntries CreateAuditorEntries<T>(GenericEntities<T> context) where T : DbContext;
    }
}
