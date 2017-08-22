using backend_tcc.lib.repository.Interfaces;
using System;

namespace backend_tcc.lib.repository.Class
{
    public class ServiceFacadeBase<TFactoryDatabse> : Disposable, IServiceFacadeBase where TFactoryDatabse : IDatabaseFactory
    {
        protected readonly IDatabaseFactory FactoryDatabase;
        protected readonly IUnitOfWork UnitOfWork;
        protected readonly IDatabaseContext DbContext;

        public event Action OnBeginCommit;
        public event Action OnEndCommit;

        public ServiceFacadeBase(TFactoryDatabse factory, IUnitOfWorkGeneric<TFactoryDatabse> unitOfWork)
        {
            this.FactoryDatabase = factory;
            this.UnitOfWork = unitOfWork;
            this.DbContext = factory.GetContext();
        }

        public int Commit(string userID)
        {
            var result = UnitOfWork.Commit(userID);
            return result;
        }

        public void Rollback()
        {
            UnitOfWork.Rollback();
        }
    }
}
