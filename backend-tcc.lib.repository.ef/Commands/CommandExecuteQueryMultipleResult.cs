using System;
using System.Data;
using System.Data.Common;
using System.Data.Entity;

namespace backend_tcc.lib.repository.ef.Class
{
    /// <summary>
    /// Classe responsável por obter múltiplos resultados de uma única consulta ao BD
    /// </summary>
    public class CommandExecuteQueryMultipleResult : CommandBaseFluentApi<CommandExecuteQueryMultipleResult>
    {
        #region Fields
        private CallbackDataReader Callback;
        private int QtdResult;
        #endregion

        #region Constructors
        public CommandExecuteQueryMultipleResult(DbContext db, CallbackDataReader callback)
            : base(db)
        {
            this.Callback = callback;
        }
        #endregion

        #region Delegates
        public delegate void CallbackDataReader(DbDataReader reader, int resultIndex);
        #endregion

        #region Fluent Api Methods
        public CommandExecuteQueryMultipleResult AddDelegate(Delegate d)
        {
            return this;
        }

        public CommandExecuteQueryMultipleResult SetQtdResult(int qtd)
        {
            this.QtdResult = qtd;
            return this;
        }
        #endregion

        #region Methods
        public override void Execute()
        {
            try
            {
                var cmd = this.CreateCommand();

                if (db.Database.Connection.State != ConnectionState.Open)
                    db.Database.Connection.Open();

                // Run the sproc  
                var reader = cmd.ExecuteReader();

                var resultindex = 0;

                do
                {
                    if (reader.Read() == false)
                        continue;
                    Callback(reader, resultindex++);

                } while (
                    (QtdResult == 0 || resultindex < QtdResult) &&
                    reader.NextResult());
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
