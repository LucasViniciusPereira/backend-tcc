using System.Data.Entity;
using System.Linq;

namespace backend_tcc.lib.repository.ef.Class
{
    /// <summary>
    /// Classe responsável por execução de query e materialização do objeto genérico
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CommandExecuteQuery<T> : CommandBaseFluentApi<CommandExecuteQuery<T>> where T : class
    {
        #region Properties
        public IQueryable<T> Result { get; private set; }
        #endregion

        #region Construcors
        public CommandExecuteQuery(DbContext db)
            : base(db)
        {

        }
        #endregion

        #region Methods
        public override void Execute()
        {
            db.Database.CommandTimeout = CommandTimeout;
            var newCmd = CommandText;

            if (CommandType == System.Data.CommandType.StoredProcedure)
                newCmd = string.Format("exec {0} {1}", CommandText, string.Join(",", this.Parameters.Select(c => c.ParameterName).ToArray()));

            var result = db.Database.SqlQuery<T>(newCmd, this.Parameters);
            this.Result = result.AsQueryable<T>();
        }
        #endregion
    }
}
