using backend_tcc.lib.repository.Interfaces;
using System;

namespace backend_tcc.lib.repository.Class
{
    public class UnitOfWork<TDatabaseFactory> : IUnitOfWorkGeneric<TDatabaseFactory>
                            where TDatabaseFactory : IDatabaseFactory
    {
        private readonly IDatabaseFactory databaseFactory;
        private IUnitOfWork dataContext;

        public UnitOfWork(TDatabaseFactory databaseFactory)
        {
            this.databaseFactory = databaseFactory;
            this.dataContext = DataContext;
        }

        protected IUnitOfWork DataContext
        {
            get { return dataContext ?? (dataContext = (IUnitOfWork)databaseFactory.GetContext()); }
        }

        public int Commit(string UserID)
        {
            try
            {
                return DataContext.Commit(UserID);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public void Rollback()
        {
            DataContext.Rollback();
        }
    }
}
