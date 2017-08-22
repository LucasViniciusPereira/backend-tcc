using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;

namespace backend_tcc.lib.repository.ef.Class
{
    public class CommandExecuteBatch : CommandBase
    {
        #region Fields
        private string DestinationTable;
        private DataTable dataTable = new DataTable();
        private bool executed = false;
        #endregion

        #region Constructors
        public CommandExecuteBatch(DbContext db, string destinationTable, Dictionary<string, Type> LstColumnNameType, List<object[]> LstValues)
            : base(db)
        {
            try
            {
                this.DestinationTable = destinationTable;
                dataTable.Columns.AddRange(LstColumnNameType.Select(c => new DataColumn(c.Key, Nullable.GetUnderlyingType(c.Value) ?? c.Value)).ToArray());

                foreach (DataColumn item in dataTable.Columns)
                {
                    if (LstColumnNameType.Any(c => c.Key == item.ColumnName && Nullable.GetUnderlyingType(c.Value) != null))
                        item.AllowDBNull = true;
                }

                foreach (var item in LstValues.AsParallel())
                {
                    dataTable.Rows.Add(item);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Methods
        public override void Execute()
        {
            if (db.Database.CurrentTransaction == null)
                throw new ApplicationException("A transação atual não foi atribuída em ExecuteBatch");

            if (dataTable.Rows.Count == 0)
                throw new ApplicationException("Não há registros a serem processados em ExecuteBatch");

            if (executed)
                throw new ApplicationException("Este comando não pode ser executado uma única vez em ExecuteBatch");

            SqlBulkCopy batchCopy = new SqlBulkCopy((SqlConnection)db.Database.Connection, SqlBulkCopyOptions.CheckConstraints, (SqlTransaction)db.Database.CurrentTransaction.UnderlyingTransaction);

            try
            {
                batchCopy.DestinationTableName = DestinationTable;

                foreach (var column in dataTable.Columns)
                    batchCopy.ColumnMappings.Add(column.ToString(), column.ToString());

                batchCopy.WriteToServer(dataTable);

                dataTable.Clear();
                dataTable.AcceptChanges();

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                batchCopy.Close();
            }

        }
        #endregion

        public override void Dispose()
        {
            dataTable.Dispose();
            base.Dispose();
        }
    }
}
