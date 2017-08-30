using backend_tcc.bs.marketing.Class;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace backend_tcc.lib.repository.Configuration.Marketing
{
    public class ItemPromocaoConfiguration : EntityTypeConfiguration<ItemPromocao>
    {
        public ItemPromocaoConfiguration()
        {
            HasKey(c => c.ItemPromocaoID);
            Property(c => c.ItemPromocaoID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Map(c => c.ToTable("ItemPromocao", "api"));

            HasRequired(c => c.Promocao)
                .WithMany()
                .HasForeignKey(f => f.PromocaoID);

            HasRequired(c => c.Produto)
                .WithMany()
                .HasForeignKey(f => f.ProdutoID);

        }
    }
}
