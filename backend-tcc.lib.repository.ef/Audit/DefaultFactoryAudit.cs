using backend_tcc.lib.repository.ef.Class;
using backend_tcc.lib.repository.ef.Interface;
using System.Data.Entity;

namespace backend_tcc.lib.repository.ef.Audit
{
    public class DefaultFactoryAudit : IFactoryAudit
    {
        private static IFactoryAudit defaultFactoryAudit = new DefaultFactoryAudit();

        public static IFactoryAudit Instance
        {
            get
            {
                return defaultFactoryAudit;
            }
        }

        public IAuditorEntries CreateAuditorEntries<T>(GenericEntities<T> context) where T : DbContext
        {
            return new AuditEntries<T>(context);
        }
    }
}
