using backend_tcc.bs.estoque.Class;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace backend_tcc.lib.repository.Configuration
{
    public class ProdutoConfiguration : EntityTypeConfiguration<Produto>
    {
        public ProdutoConfiguration()
        {
            HasKey(c => c.ProdutoID);
            Property(c => c.ProdutoID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Map(c => c.ToTable("Produto"));

            HasRequired(c => c.Fabricante)
                .WithMany()
                .HasForeignKey(f => f.FabricanteID);

            HasOptional(c => c.Fornecedor)
                .WithMany()
                .HasForeignKey(f => f.FornecedorID);
        }
    }
}

