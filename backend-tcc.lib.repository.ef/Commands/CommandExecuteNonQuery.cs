using System;
using System.Data;
using System.Data.Entity;

namespace backend_tcc.lib.repository.ef.Class
{
    /// <summary>
    /// Classe responsável por executar um comando
    /// </summary>
    public class CommandExecuteNonQuery : CommandBaseFluentApi<CommandExecuteNonQuery>
    {
        #region Properties
        public int ResultExecuteNonQuery { get; private set; }
        #endregion

        #region Constructors
        public CommandExecuteNonQuery(DbContext db)
            : base(db)
        {

        }
        #endregion

        #region Methods
        public override void Execute()
        {

            try
            {
                using (var cmd = this.CreateCommand())
                {
                    if (this.db.Database.Connection.State != ConnectionState.Open)
                        this.db.Database.Connection.Open();
                    ResultExecuteNonQuery = cmd.ExecuteNonQuery();
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
