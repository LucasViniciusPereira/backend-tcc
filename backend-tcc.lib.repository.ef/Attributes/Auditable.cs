using System;

namespace backend_tcc.lib.repository.ef
{
    /// <summary>
    /// Atributo responsável por identificar se um DbSet será auditável
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Property,
                           AllowMultiple = true)
    ]
    public class Auditable : System.Attribute
    {
        public Auditable(Type tipo)
        {
            this.Tipo = tipo;
        }

        public Type Tipo { get; set; }

    }
}
