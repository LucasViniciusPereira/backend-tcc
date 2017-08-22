using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;

namespace backend_tcc.lib.repository.ef.Class
{
    public abstract class CommandBaseFluentApi<T> : CommandBase where T : CommandBaseFluentApi<T>
    {
        #region Fields
        protected int CommandTimeout = 30;
        protected string CommandText;
        private List<DatabaseCommandParameter> parameters = new List<DatabaseCommandParameter>();
        protected CommandType CommandType = CommandType.Text;
        #endregion

        #region Properties
        protected DbParameter[] Parameters { get { return parameters.ToNativeType<DbParameter>(); } }
        #endregion

        #region Constructors
        public CommandBaseFluentApi(DbContext context)
            : base(context)
        {

        }
        #endregion

        #region Fluent Api Methods
        public T SetTimeout(int commandTimeout)
        {
            this.CommandTimeout = commandTimeout;
            return (T)this;
        }

        public T SetCommandType(CommandType commandType)
        {
            this.CommandType = commandType;
            return (T)this;
        }

        public T SetCommandText(string commandText)
        {
            this.CommandText = commandText;
            return (T)this;
        }

        public T SetCommandText(Enum commandEnum)
        {
            this.CommandText = commandEnum.GetDescricao();
            return (T)this;
        }

        public T AddParameter(DatabaseCommandParameter parameter)
        {
            if (parameter.NativeParameter.Value == null)
                parameter.NativeParameter.Value = DBNull.Value;

            this.parameters.Add(parameter);
            return (T)this;
        }

        public T AddParameter(List<DatabaseCommandParameter> parameters)
        {
            foreach (var parameter in parameters)
            {
                this.AddParameter(parameter);
            }

            return (T)this;
        }

        public T RemoveParameter(string parameterName)
        {
            this.parameters.SingleOrDefault(c => c.NativeParameter.ParameterName == parameterName);
            return (T)this;
        }
        #endregion

        #region Methods
        protected DbCommand CreateCommand()
        {
            try
            {
                var cmd = this.db.Database.Connection.CreateCommand();

                cmd.Parameters.AddRange(this.parameters.Select(d => (DbParameter)d).ToArray());
                cmd.CommandTimeout = this.CommandTimeout;

                var newCmdText = CommandText;
                if (CommandType == System.Data.CommandType.StoredProcedure)
                    newCmdText = string.Format("exec {0} {1}", CommandText, string.Join(",", this.Parameters.Where(d => d.Value != DBNull.Value).Select(c => c.ParameterName).ToArray()));

                cmd.CommandText = newCmdText;

                if (this.db.Database.CurrentTransaction != null)
                    cmd.Transaction = this.db.Database.CurrentTransaction.UnderlyingTransaction;

                return cmd;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }

}