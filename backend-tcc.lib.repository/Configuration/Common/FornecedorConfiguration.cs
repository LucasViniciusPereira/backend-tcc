using backend_tcc.bs.common.Class;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace backend_tcc.lib.repository.Configuration.Common
{
    public class FornecedorConfiguration : EntityTypeConfiguration<Fornecedor>
    {
        public FornecedorConfiguration()
        {
            HasKey(c => c.FornecedorID);
            Property(c => c.FornecedorID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Map(c => c.ToTable("Fornecedor", "api"));
        }
    }
}
