using backend_tcc.lib.Class;
using System.Data.Entity;

namespace backend_tcc.lib.repository.ef.Class
{
    public abstract class CommandBase : Disposable, ICommandDisposable
    {
        #region Constructors
        public CommandBase(DbContext context)
        {
            this.db = context;
        }
        #endregion

        #region Fields
        protected DbContext db;
        #endregion

        #region Methods
        public abstract void Execute();

        #endregion
    }
}

