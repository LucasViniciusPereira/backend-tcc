using backend_tcc.bs.logistica.Class;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace backend_tcc.lib.repository.Configuration.Logistica
{
    public class ItemEntregaConfiguration : EntityTypeConfiguration<ItemEntrega>
    {
        public ItemEntregaConfiguration()
        {
            HasKey(c => c.ItemEntregaID);
            Property(c => c.ItemEntregaID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Map(c => c.ToTable("ItemEntrega"));

            HasRequired(c => c.Entrega)
                .WithMany()
                .HasForeignKey(f => f.EntregaID);

            HasRequired(c => c.Pedido)
                .WithMany()
                .HasForeignKey(f => f.PedidoID);

        }
    }
}
