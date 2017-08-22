using backend_tcc.lib.repository.ef.Audit;
using backend_tcc.lib.repository.ef.Class;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace backend_tcc.lib.repository.ef
{
    /// <summary>
    /// Extensão para verificar se as entidades DbSet possuem o atributo [Auditable]
    /// </summary>
    public static class ExtensionEF
    {
        public static Type GetPocoType(this DbEntityEntry entry)
        {
            Type type = null;
            if (entry.Entity.GetType().FullName.StartsWith("System.Data.Entity.DynamicProxies."))
                type = entry.Entity.GetType().BaseType;
            else
                type = entry.Entity.GetType();

            return type;
        }

        public static bool IsAuditable(this List<AuditableProperties> array, DbEntityEntry entry)
        {
            //System.Data.Entity.
            Type type = entry.GetPocoType();

            var result = array.Any(d => d.TypeClass == type);

            return result;
        }

        public static T[] ToNativeType<T>(this List<DatabaseCommandParameter> lst) where T : DbParameter
        {
            return lst.Select(c => c.NativeParameter).OfType<T>().ToArray();
        }

        public static string GetKeyValue(this DbPropertyValues propertyValues, AuditableProperties property)
        {
            var values = property
                        .KeyNameCSpace
                        .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(key => propertyValues.GetValue<object>(key))
                        .ToList();

            var result = string.Join(",", values);

            return result;
        }
    }
}