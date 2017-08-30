using backend_tcc.bs.logistica.Class;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace backend_tcc.lib.repository.Configuration.Logistica
{
    public class EntregaConfiguration : EntityTypeConfiguration<Entrega>
    {
        public EntregaConfiguration()
        {
            HasKey(c => c.EntregaID);
            Property(c => c.EntregaID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Map(c => c.ToTable("Entrega", "api"));

            HasRequired(c => c.Veiculo)
                .WithMany()
                .HasForeignKey(f => f.VeiculoID);

            HasMany(c => c.Itens)
                .WithOptional()
                .HasForeignKey(f => f.EntregaID);

        }
    }
}
