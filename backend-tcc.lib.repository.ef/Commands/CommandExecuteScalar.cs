using System;
using System.Data;
using System.Data.Entity;

namespace backend_tcc.lib.repository.ef.Class
{
    /// <summary>
    /// Classe responsável por execução com retorno escalar
    /// </summary>
    public class CommandExecuteScalar : CommandBaseFluentApi<CommandExecuteScalar>
    {
        #region Properties
        public object ScalarValueResult { get; private set; }
        #endregion

        #region Constructors
        public CommandExecuteScalar(DbContext db)
            : base(db)
        {

        }
        #endregion

        #region Methods
        public override void Execute()
        {
            if (db.Database.CurrentTransaction == null)
                throw new ApplicationException("A transação atual não foi atribuída em ExecuteBatch");

            try
            {
                using (var cmd = this.CreateCommand())
                {
                    if (this.db.Database.Connection.State != ConnectionState.Open)
                        this.db.Database.Connection.Open();
                    ScalarValueResult = cmd.ExecuteScalar();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
