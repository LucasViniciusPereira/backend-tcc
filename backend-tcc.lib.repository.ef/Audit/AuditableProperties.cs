using System;

namespace backend_tcc.lib.repository.ef.Audit
{
    public class AuditableProperties
    {
        /// <summary>
        /// Tipo da classe
        /// </summary>
        public Type TypeClass { get; set; }
        /// <summary>
        /// Nome da tabela
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// Nome da PK do BD
        /// </summary>
        public string KeyNameSSpace { get; set; }
        /// <summary>
        /// Nome da EntityKey
        /// </summary>
        public string KeyNameCSpace { get; set; }
    }
}
