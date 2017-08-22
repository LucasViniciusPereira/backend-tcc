using System;
using System.ComponentModel.DataAnnotations;

namespace backend_tcc.lib.repository.ef.Audit
{
    public class AuditLog
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Transaction">Agrupa todas os registros de auditoria executados em uma transação</param>
        public AuditLog(Guid Transaction)
        {
            this.TransactionId = Transaction;
        }

        [Key]
        public Guid AuditLogID { get; set; }

        public Guid TransactionId { get; set; }

        [Required]
        [MaxLength(50)]
        public string UserID { get; set; }

        [Required]
        public DateTimeOffset EventDateUTC { get; set; }

        [Required]
        [MaxLength(1)]
        public string EventType { get; set; }

        [Required]
        [MaxLength(100)]
        public string TableName { get; set; }

        [Required]
        [MaxLength(100)]
        public string RecordID { get; set; }

        [Required]
        [MaxLength(100)]
        public string ColumnName { get; set; }

        public string OriginalValue { get; set; }

        public string NewValue { get; set; }
    }
}
