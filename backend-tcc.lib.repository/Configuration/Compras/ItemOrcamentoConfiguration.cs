using backend_tcc.bs.compras.Class;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace backend_tcc.lib.repository.Configuration.Compras
{
    public class ItemOrcamentoConfiguration : EntityTypeConfiguration<ItemOrcamento>
    {
        public ItemOrcamentoConfiguration()
        {
            HasKey(c => c.ItemOrcamentoID);
            Property(c => c.ItemOrcamentoID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Map(c => c.ToTable("ItemOrcamento"));

            HasRequired(c => c.Produto)
                .WithMany()
                .HasForeignKey(f => f.ProdutoID);

            HasRequired(c => c.Orcamento)
                .WithMany()
                .HasForeignKey(f => f.OrcamentoID);
        }
    }
}
