using System.Data;
using System.Data.SqlClient;

namespace backend_tcc.lib.repository.ef.Class
{
    public class DatabaseCommandParameter
    {
        private SqlParameter Parameter;

        public SqlParameter NativeParameter { get { return Parameter; } }

        private DatabaseCommandParameter(SqlParameter p)
        {
            Parameter = p;
        }

        public static implicit operator DatabaseCommandParameter(SqlParameter p)
        {
            return new DatabaseCommandParameter(p);
        }

        public static implicit operator SqlParameter(DatabaseCommandParameter dp)
        {
            return dp.Parameter;
        }



        public static DatabaseCommandParameter CreateParameter(string name, DbType tipo, object value, int size, byte scale, byte precision)
        {
            return new SqlParameter
            {
                ParameterName = name,
                DbType = tipo,
                Value = value,
                Precision = (byte)precision,
                Scale = (byte)scale,
                Size = (int)size
            };
        }

        public static DatabaseCommandParameter CreateParameter(string name, DbType tipo, object value, int size)
        {
            return new SqlParameter
            {
                ParameterName = name,
                DbType = tipo,
                Value = value,
                Size = size
            };
        }

        public static DatabaseCommandParameter CreateParameter(string name, DbType tipo, object value, byte scale, byte precision)
        {
            return new SqlParameter
            {
                ParameterName = name,
                DbType = tipo,
                Value = value,
                Precision = precision,
                Scale = scale
            };
        }

        public static DatabaseCommandParameter CreateParameter(string name, DbType tipo, object value)
        {
            return new SqlParameter
            {
                ParameterName = name,
                DbType = tipo,
                Value = value
            };
        }
    }
}
