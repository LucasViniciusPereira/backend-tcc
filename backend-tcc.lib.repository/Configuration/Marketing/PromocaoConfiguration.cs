using backend_tcc.bs.marketing.Class;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace backend_tcc.lib.repository.Configuration.Marketing
{
    public class PromocaoConfiguration : EntityTypeConfiguration<Promocao>
    {
        public PromocaoConfiguration()
        {
            HasKey(c => c.PromocaoID);
            Property(c => c.PromocaoID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Map(c => c.ToTable("Promocao", "api"));

            HasOptional(c => c.Cliente)
                .WithMany()
                .HasForeignKey(f => f.ClienteID);

            HasMany(c => c.Itens)
                .WithOptional()
                .HasForeignKey(f => f.PromocaoID);

        }
    }
}