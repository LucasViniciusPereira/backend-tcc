using backend_tcc.bs.estoque.Class;
using System.Data.Entity.ModelConfiguration;

namespace backend_tcc.lib.repository.Configuration
{
    public class EstoqueConfiguration : EntityTypeConfiguration<Estoque>
    {
        public EstoqueConfiguration()
        {
            HasKey(c => c.ProdutoID);
            //Property(c => c.ProdutoID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Map(c => c.ToTable("Estoque"));

            HasRequired(c => c.Produto)
                .WithRequiredDependent();
                //.HasForeignKey(f => f.ProdutoID);

            HasMany(c => c.Lotes)
                .WithOptional()
                .HasForeignKey(f => f.ProdutoID);

        }
    }
}
