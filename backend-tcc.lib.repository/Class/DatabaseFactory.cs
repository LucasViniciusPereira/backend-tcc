using backend_tcc.lib.repository.Interfaces;
using System;

namespace backend_tcc.lib.repository.Class
{
    public class DatabaseFactory : IDatabaseFactory, IDisposable
    {

        private IDatabaseContext dataContext;
        private Type type;
        private string connectionString;

        public DatabaseFactory(Type _type, string cnnString)
        {
            this.type = _type;
            this.connectionString = cnnString;
            this.dataContext = this.GetContext();
        }

        public IDatabaseContext GetContext()
        {
            var result = dataContext ?? (dataContext = (IDatabaseContext)Activator.CreateInstance(type, new object[] { connectionString }));

            return result;
        }

        public void Dispose()
        {
            if (dataContext != null)
                dataContext.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}
