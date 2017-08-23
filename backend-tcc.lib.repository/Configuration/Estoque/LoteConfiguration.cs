using backend_tcc.bs.estoque.Class;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace backend_tcc.lib.repository.Configuration
{
    public class LoteConfiguration : EntityTypeConfiguration<Lote>
    {
        public LoteConfiguration()
        {
            HasKey(c => c.LoteID);
            Property(c => c.LoteID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Map(c => c.ToTable("Lote"));

            HasRequired(c => c.Fabricante)
                .WithMany()
                .HasForeignKey(f => f.Fabricante);

            HasRequired(c => c.Produto)
                .WithMany()
                .HasForeignKey(f => f.ProdutoID);

        }
    }
}