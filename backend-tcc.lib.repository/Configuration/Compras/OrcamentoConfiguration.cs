using backend_tcc.bs.compras.Class;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace backend_tcc.lib.repository.Configuration.Compras
{
    public class OrcamentoConfiguration : EntityTypeConfiguration<Orcamento>
    {
        public OrcamentoConfiguration()
        {
            HasKey(c => c.OrcamentoID);
            Property(c => c.OrcamentoID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Map(c => c.ToTable("Orcamento", "api"));

            HasRequired(c => c.Fornecedor)
                .WithMany()
                .HasForeignKey(f => f.FornecedorID);

            HasMany(c => c.Itens)
                .WithRequired()
                .HasForeignKey(f => f.OrcamentoID);
                
        }
    }
}
